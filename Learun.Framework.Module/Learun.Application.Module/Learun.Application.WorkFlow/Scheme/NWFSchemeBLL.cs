using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板(新)
    /// </summary>
    public class NWFSchemeBLL: NWFSchemeIBLL
    {
        private NWFSchemeService nWFSchemeService = new NWFSchemeService();

        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoPageList(Pagination pagination, string queryJson)
        {
            return nWFSchemeService.GetInfoPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoList(UserInfo userInfo)
        {
            return nWFSchemeService.GetInfoList(userInfo);
        }
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoList()
        {
            return nWFSchemeService.GetInfoList();
        }
        /// <summary>
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetAppInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson)
        {
            return nWFSchemeService.GetAppInfoPageList(pagination, userInfo, queryJson);
        }

        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFSchemeInfoEntity GetInfoEntity(string keyValue)
        {
            return nWFSchemeService.GetInfoEntity(keyValue);
        }
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public NWFSchemeInfoEntity GetInfoEntityByCode(string code)
        {
            return nWFSchemeService.GetInfoEntityByCode(code);
        }

        /// <summary>
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeAuthEntity> GetAuthList(string schemeInfoId)
        {
            return nWFSchemeService.GetAuthList(schemeInfoId);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程信息主键</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            return nWFSchemeService.GetSchemePageList(pagination, schemeInfoId);
        }
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFSchemeEntity GetSchemeEntity(string keyValue)
        {
            return nWFSchemeService.GetSchemeEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            nWFSchemeService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="infoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        /// <param name="authList">模板权限信息</param>
        public void SaveEntity(string keyValue, NWFSchemeInfoEntity infoEntity, NWFSchemeEntity schemeEntity, List<NWFSchemeAuthEntity> authList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                NWFSchemeEntity oldNWFSchemeEntity = GetSchemeEntity(infoEntity.F_SchemeId);
                if (oldNWFSchemeEntity.F_Content == schemeEntity.F_Content && oldNWFSchemeEntity.F_Type == schemeEntity.F_Type)
                {
                    schemeEntity = null;
                }
            }
            nWFSchemeService.SaveEntity(keyValue, infoEntity, schemeEntity, authList);
        }
        /// <summary>
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public void UpdateScheme(string schemeInfoId, string schemeId)
        {
            nWFSchemeService.UpdateScheme(schemeInfoId, schemeId);
        }
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string schemeInfoId, int state)
        {
            nWFSchemeService.UpdateState(schemeInfoId, state);
        }
        #endregion
    }
}
