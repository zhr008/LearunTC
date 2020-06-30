using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.Message
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public interface LR_StrategyInfoIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_MS_StrategyInfoEntity> GetList( string queryJson );
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_MS_StrategyInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_MS_StrategyInfoEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据策略编码获取策略
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <returns></returns>
        LR_MS_StrategyInfoEntity GetEntityByCode(string code);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, LR_MS_StrategyInfoEntity entity);
        /// <summary>
        ///策略编码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_StrategyCode">编码</param>
        /// <returns></returns>
        bool ExistStrategyCode(string keyValue, string F_StrategyCode);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 消息处理，在此处处理好数据，然后调用消息发送方法
        /// </summary>
        /// <param name="code">消息策略编码</param>
        /// <param name="content">消息内容</param>
        /// <param name="userlist">用户列表信息</param>
        /// <returns></returns>
        ResParameter SendMessage(string code, string content, string userlist);
        #endregion
    }
}
