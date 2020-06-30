using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Language
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:08
    /// 描 述：语言类型
    /// </summary>
    public interface LGTypeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取语言类型全部数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGTypeEntity> GetAllData();
        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGTypeEntity> GetList( string queryJson );
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGTypeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LGTypeEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LGTypeEntity GetEntityByCode(string keyValue);
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
        void SaveEntity(string keyValue, LGTypeEntity entity);
        /// <summary>
        /// 设为主语言
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SetMainLG(string keyValue);
        
        #endregion

    }
}
