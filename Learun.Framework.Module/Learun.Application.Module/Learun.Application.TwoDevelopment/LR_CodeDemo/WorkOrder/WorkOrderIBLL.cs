using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-10 17:21
    /// 描 述：工单管理
    /// </summary>
    public interface WorkOrderIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_Demo_WorkOrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取屏幕显示数据
        /// <summary>
        /// <returns></returns>
        DataTable GetList();
        /// <summary>
        /// 获取套打数据
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        DataTable GetPrintItem(string keyValue);
        /// <summary>
        /// 获取LR_Demo_WorkOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_Demo_WorkOrderEntity GetLR_Demo_WorkOrderEntity(string keyValue);
        /// <summary>
        /// 获取主表实体数据
        /// <param name="processId">流程实例ID</param>
        /// <summary>
        /// <returns></returns>
        LR_Demo_WorkOrderEntity GetEntityByProcessId(string processId);
        /// <summary>
        /// 获取左侧树形数据
        /// <summary>
        /// <returns></returns>
        List<TreeModel> GetTree();
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
        void SaveEntity(string keyValue, LR_Demo_WorkOrderEntity entity);
        #endregion

    }
}
