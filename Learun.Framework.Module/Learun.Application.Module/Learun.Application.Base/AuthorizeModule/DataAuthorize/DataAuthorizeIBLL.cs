using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：力软框架开发组
    /// 日 期：2017-06-21 16:30
    /// 描 述：数据权限
    /// </summary>
    public interface DataAuthorizeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取条件列表数据
        /// </summary>
        /// <param name="relationId">关系主键</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeConditionEntity> GetDataAuthorizeConditionList(string relationId);
        /// <summary>
        /// 获取数据权限对应关系数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeRelationEntity> GetRelationList(string moduleId);
        /// <summary>
        /// 获取数据权限对应关系数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">对象主键</param>
        /// <param name="type">类型 2 自定义表单</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeRelationEntity> GetRelationPageList(Pagination pagination, string moduleId, string keyword, string objectId,int type);
        /// <summary>
        /// 获取数据权限条件列
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <param name="objectId">对应角色或用户主键</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeConditionEntity> GetConditionList(string moduleId, string objectId);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        DataAuthorizeRelationEntity GetRelationEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="relationEntity"></param>
        /// <param name="conditionEntityList"></param>
        void SaveEntity(string keyValue, DataAuthorizeRelationEntity relationEntity, List<DataAuthorizeConditionEntity> conditionEntityList);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 设置查询语句
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="isCustmerForm">是否是自定义表单</param>
        /// <returns></returns>
        bool SetWhereSql(string url, bool isCustmerForm);
        #endregion
    }
}
