using Learun.Application.AppMagager;
using Learun.Application.Base.SystemModule;
using Learun.Util;
using Nancy;
using System.Collections.Generic;

namespace Learun.Application.WebApi.Modules
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-06-26 10:32 
    /// 描 述：移动应用 
    /// </summary> 
    public class FunctionApi : BaseApi
    {

        public FunctionApi()
        : base("/learun/adms/function")
        {
            Get["/list"] = GetList;
            Get["/mylist"] = GetMyList;
            Get["/modules"] = GetModuleList;

            Post["/mylist/update"] = UpdateMyList;

            
        }

        private ModuleIBLL moduleIBLL = new ModuleBLL();

        private MyFunctionIBLL myFunctionIBLL = new MyFunctionBLL();
        private FunctionIBLL functionIBLL = new FunctionBLL();

        /// <summary>
        /// 获取全部移动功能数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetList(dynamic _)
        {
            string ver = this.GetReqData();// 获取模板请求数据

            var list = functionIBLL.GetList(this.userInfo);

            string md5 = Md5Helper.Encrypt(list.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = list,
                    ver = md5
                };
                return Success(jsondata);
            }
        }
        /// <summary>
        /// 获取我的常用应用数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetMyList(dynamic _)
        {

            var list = myFunctionIBLL.GetList(this.userInfo.userId);
            List<string> res = new List<string>();
            foreach (var item in list) {
                res.Add(item.F_FunctionId);
            }
            return Success(res);
        }

        /// <summary>
        /// 保存我的常用应用
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response UpdateMyList(dynamic _) {
            string strFunctionId = this.GetReqData();// 获取模板请求数据
            myFunctionIBLL.SaveEntity(this.userInfo.userId, strFunctionId);
            return Success("保存成功");
        }

        /// <summary>
        /// 获取系统功能模块
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetModuleList(dynamic _)
        {
            var data = moduleIBLL.GetModuleList(userInfo);
            return Success(data);
        }
    }
}