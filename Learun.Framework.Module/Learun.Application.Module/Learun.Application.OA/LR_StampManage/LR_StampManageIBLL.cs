using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.OA.LR_StampManage
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组（王飞）
    /// 日 期：2018.11.19
    /// 描 述：印章管理（接口）
    /// </summary>
    public interface LR_StampManageIBLL
    {
        #region 获取数据
        /// <summary>
        /// 模糊查询（根据名称/状态（启用或者停用））
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IEnumerable<LR_StampManageEntity> GetList(string keyWord);
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_StampManageEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取印章树形数据
        /// </summary>
        /// <param name="parentId">父级节点</param>
        /// <returns></returns>
        LR_StampManageEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="stampEntity"></param>
        void SaveEntity(string keyValue, LR_StampManageEntity stampEntity);

        /// <summary>
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue"></param>
        void DeleteEntity(string keyVlaue);

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        void UpdateState(string keyValue, int state);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg(string keyValue);
        /// <summary>
        /// 密码匹配
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool EqualPassword(string keyValue, string Password);
        #endregion
    }
}
