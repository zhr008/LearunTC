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
    public interface NWFSchemeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeInfoEntity> GetInfoPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeInfoEntity> GetInfoList(UserInfo userInfo);
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeInfoEntity> GetInfoList();
        /// <summary>
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeInfoEntity> GetAppInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson);
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        NWFSchemeInfoEntity GetInfoEntity(string keyValue);
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        NWFSchemeInfoEntity GetInfoEntityByCode(string code);
        /// <summary>
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeAuthEntity> GetAuthList(string schemeInfoId);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程信息主键</param>
        /// <returns></returns>
        IEnumerable<NWFSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId);
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        NWFSchemeEntity GetSchemeEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="infoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        /// <param name="authList">模板权限信息</param>
        void SaveEntity(string keyValue, NWFSchemeInfoEntity infoEntity, NWFSchemeEntity schemeEntity, List<NWFSchemeAuthEntity> authList);
        /// <summary>
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        void UpdateScheme(string schemeInfoId, string schemeId);
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        void UpdateState(string schemeInfoId, int state);
        #endregion
    }
}
