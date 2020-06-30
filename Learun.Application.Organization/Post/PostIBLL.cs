using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Organization
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：岗位管理
    /// </summary>
    public interface PostIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        List<PostEntity> GetList(string companyId);
        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="keyword">关键词</param>
        /// <param name="keyword">部门Id</param>
        /// <returns></returns>
        List<PostEntity> GetList(string companyId, string keyword, string departmentId);
        /// <summary>
        /// 获取岗位数据列表(根据主键串)
        /// </summary>
        /// <param name="postIds">根据主键串</param>
        /// <returns></returns>
        IEnumerable<PostEntity> GetListByPostIds(string postIds);
         /// <summary>
        /// 获取树形结构数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        List<TreeModel> GetTree(string companyId);
        /// <summary>
        /// 获取岗位实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        PostEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDelete(string keyValue);
        /// <summary>
        /// 保存岗位（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="postEntity">岗位实体</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, PostEntity postEntity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断是否是上级
        /// </summary>
        /// <param name="myId">自己的岗位</param>
        /// <param name="otherId">对方的岗位</param>
        /// <returns></returns>
        bool IsUp(string myId, string otherId);
        /// <summary>
        /// 判断是否是下级
        /// </summary>
        /// <param name="myId">自己的岗位</param>
        /// <param name="otherId">对方的岗位</param>
        /// <returns></returns>
        bool IsDown(string myId, string otherId);
        /// <summary>
        /// 获取上级岗位人员ID
        /// </summary>
        /// <param name="strPostIds">岗位id</param>
        /// <param name="level">级数</param>
        /// <returns></returns>
        List<string> GetUpIdList(string strPostIds, int level);
        /// <summary>
        /// 获取下级岗位人员ID
        /// </summary>
        /// <param name="strPostIds">岗位id</param>
        /// <param name="level">级数</param>
        /// <returns></returns>
        List<string> GetDownIdList(string strPostIds, int level);
        #endregion
    }
}
