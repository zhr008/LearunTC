using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:56
    /// 描 述：个人资格证
    /// </summary>
    public interface CredentialsIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<CredentialsInfo> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取tc_Credentials表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue);
        /// <summary>
        /// 获取tc_Credentials表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue, int? F_CertType, int? F_MajorType);
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
        void SaveEntity(string keyValue, tc_CredentialsEntity entity);
        #endregion

    }
}
