using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 15:11
    /// 描 述：单据历史
    /// </summary>
    public class DataHistoryService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public DataHistoryService()
        {
            fieldSql=@"
                t.F_Id,
                t.F_PurchaseNo,
                t.F_Appler,
                t.F_Department,
                t.F_PurchaseType,
                t.F_ApplyDate,
                t.F_CreateDate,
                t.F_Description,
                t.F_File,
                t.F_Theme,
                t.F_Code,
                t.F_Name,
                t.F_BarCode,
                t.F_Place,
                t.F_Specification,
                t.F_Type,
                t.F_Number,
                t.F_Unit,
                t.F_Count,
                t.F_Price,
                t.F_Amount,
                t.F_Status,
                t.F_Purchaser,
                t.F_PaymentType,
                t.F_We,
                t.F_Your,
                t.F_Total,
                t.F_DeliveryDate,
                t.F_PurchaseWarehousinger,
                t.F_PurchaseWarehousingDate,
                t.F_FromAddress,
                t.F_ToAddress,
                t.F_Order,
                t.F_DataId
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_DataHistoryEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_ERP_DataHistory t ");
                return this.BaseRepository().FindList<LR_ERP_DataHistoryEntity>(strSql.ToString());
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_DataHistoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_ERP_DataHistory t ");
                return this.BaseRepository().FindList<LR_ERP_DataHistoryEntity>(strSql.ToString(), pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_DataHistoryEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_DataHistoryEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<LR_ERP_DataHistoryEntity>(t=>t.F_Id == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, LR_ERP_DataHistoryEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

    }
}
