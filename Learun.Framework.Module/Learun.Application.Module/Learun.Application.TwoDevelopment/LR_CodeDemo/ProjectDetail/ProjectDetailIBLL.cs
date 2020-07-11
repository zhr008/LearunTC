using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-10 18:11
    /// 描 述：项目详情
    /// </summary>
    public interface ProjectDetailIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<tc_ProjectDetailEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取tc_ProjectDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        tc_ProjectDetailEntity Gettc_ProjectDetailEntity(string keyValue);
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
        /// <param name="entity">实体</param>
        void SaveEntity(string keyValue, tc_ProjectDetailEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        IEnumerable<tc_ProjectDetailEntity> GetPageListByProjectId(string ProjectId);
        #endregion

    }
}
