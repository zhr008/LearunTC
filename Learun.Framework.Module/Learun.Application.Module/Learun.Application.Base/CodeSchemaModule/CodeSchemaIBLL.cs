using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.Base.CodeSchemaModule
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-01 11:09
    /// 描 述：代码模板
    /// </summary>
    public interface CodeSchemaIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_Base_CodeSchemaEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_Base_CodeSchema表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        LR_Base_CodeSchemaEntity GetLR_Base_CodeSchemaEntity(string keyValue);
        /// <summary>
        /// 获取左侧树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetTree();
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, LR_Base_CodeSchemaEntity entity);
        #endregion

    }
}
