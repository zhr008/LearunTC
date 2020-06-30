using System.Collections.Generic;

namespace Learun.Application.Base.Desktop
{
    /// <summary>
    /// 
    /// </summary>
    public interface DTListIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<DTListEntity> GetList();

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<DTListEntity> GetList(string keyword);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        DTListEntity GetEntity(string keyValue);

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
        void SaveEntity(string keyValue, DTListEntity entity);

        #endregion
    }
}
