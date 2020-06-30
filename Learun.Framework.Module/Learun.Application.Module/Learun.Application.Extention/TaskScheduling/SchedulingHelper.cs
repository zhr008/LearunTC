using Learun.Application.Base.SystemModule;
using Learun.Ioc;
using Learun.Util;
using Quartz;
using System;
using System.Threading;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public class SchedulingHelper : IJob
    {
        private TSProcessIBLL tSProcessIBLL = new TSProcessBLL();
        private TSSchemeIBLL tSSchemeIBLL = new TSSchemeBLL();
        private TSLogIBLL tSLogIBLL = new TSLogBLL();

        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();

        /// <summary>
        /// 任务执行方法
        /// </summary>
        /// <param name="keyValue">任务进程主键</param>
        /// <returns></returns>
        private bool _Execute(string keyValue) {
            bool isOk = true;
            string msg = "执行成功";

            TSProcessEntity tSProcessEntity = null;
            TSSchemeEntity tSSchemeEntity = null;

            // 获取一个任务进程
            try
            {
                tSProcessEntity = tSProcessIBLL.GetProcessEntity(keyValue);
            }
            catch (Exception ex)
            {
                isOk = false;
                msg = "获取任务进程异常：" + ex.Message;
            }

            if (tSProcessEntity != null && tSProcessEntity.F_State != 1 && tSProcessEntity.F_State != 2) {
                return true;
            }

            // 获取对应的任务模板
            if (isOk)
            {
                try
                {
                    tSSchemeEntity = tSSchemeIBLL.GetSchemeEntity(tSProcessEntity.F_SchemeId);
                }
                catch (Exception ex)
                {
                    isOk = false;
                    msg = "获取任务模板异常：" + ex.Message;
                }
            }

            bool flag = false;

            // 执行任务
            if (isOk)
            {
                try
                {
                    TSSchemeModel tSSchemeModel = tSSchemeEntity.F_Scheme.ToObject<TSSchemeModel>();
                    switch (tSSchemeModel.methodType)
                    {
                        case 1:// sql
                            databaseLinkIBLL.ExecuteBySql(tSSchemeModel.dbId, tSSchemeModel.strSql);
                            break;
                        case 2:// 存储过程
                            databaseLinkIBLL.ExecuteByProc(tSSchemeModel.dbId, tSSchemeModel.procName);
                            break;
                        case 3:// 接口
                            if (tSSchemeModel.urlType == "1")
                            {
                                HttpMethods.Get(tSSchemeModel.url);
                            }
                            else
                            {
                                HttpMethods.Post(tSSchemeModel.url);
                            }
                            break;
                        case 4:// 依赖注入
                            if (!string.IsNullOrEmpty(tSSchemeModel.iocName) && UnityIocHelper.TsInstance.IsResolve<ITsMethod>(tSSchemeModel.iocName))
                            {
                                ITsMethod iTsMethod = UnityIocHelper.TsInstance.GetService<ITsMethod>(tSSchemeModel.iocName);
                                iTsMethod.Execute();
                            }
                            break;
                    }

                    if (tSSchemeModel.executeType == 1) {
                        flag = true;
                    }

                }
                catch (Exception ex)
                {
                    isOk = false;
                    msg = "执行方法出错：" + ex.Message;
                }
            }

            try
            {
                // 新增一条任务日志
                TSLogEntity logEntity = new TSLogEntity()
                {
                    F_ExecuteResult = isOk ? 1 : 2,
                    F_Des = msg,
                    F_ProcessId = keyValue
                };
                logEntity.Create();
                tSLogIBLL.SaveEntity("", logEntity);

                if (tSProcessEntity.F_State == 1) {
                    tSProcessEntity.F_State = 2;
                    if (flag) {
                        tSProcessEntity.F_State = 4;
                    }

                    tSProcessIBLL.SaveEntity(tSProcessEntity.F_Id, tSProcessEntity);
                }
            }
            catch (Exception)
            {
            }


            return isOk;
        }

        /// <summary>
        /// 任务执行方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                string keyValue = dataMap.GetString("keyValue");

                TSProcessEntity tSProcessEntity = null;
                TSSchemeEntity tSSchemeEntity = null;

                if (!_Execute(keyValue)) { // 如果异常，需要重新执行一次
                    tSProcessEntity = tSProcessIBLL.GetProcessEntity(keyValue);
                    if (tSProcessEntity != null) {
                        tSSchemeEntity = tSSchemeIBLL.GetSchemeEntity(tSProcessEntity.F_SchemeId);
                        if (tSSchemeEntity != null)
                        {
                            TSSchemeModel tSSchemeModel = tSSchemeEntity.F_Scheme.ToObject<TSSchemeModel>();
                            if (tSSchemeModel.isRestart == 1) {
                                for (int i = 0; i < tSSchemeModel.restartNum; i++)
                                {
                                    Thread.Sleep(60 * 1000 * tSSchemeModel.restartMinute);  // 停顿1000毫秒
                                    if (_Execute(keyValue))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
