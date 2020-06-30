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
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:50
    /// 描 述：检查项目
    /// </summary>
    public class AnlyCheckService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<MSTB_QUA_CHECKITEMINFOEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.HeaderID,
                t.OrderNo,
                t.Name,
                t.Type,
                t.BaseCode,
                t.ResultNum,
                t.GroupType,
                t.GroupTime,
                t.Class,
                t.ClassOrderNo,
                t.Uom,
                t.Last_Act_Id,
                t.Last_Act_Time,
                t.Remark,
                t.Max,
                t.Min,
                t.Stander,
                t.IsRequired,
                t.Equipments,
                t.CategoryId,
                t.Accuracy,
                t.RecordStatus,
                t.RecordLastEditDt,
                t.RecordGuid,
                t.Segment,
                t.Name2,
                t.Name3,
                t.Name4,
                t.Last_Act_Name,
                t.Name5,
                t.Name6,
                t.Name7,
                t.Name8,
                t.IsAvg,
                t.IsRSD,
                t.IsStVar,
                t.IsSum,
                t.Name9,
                t.Name10,
                t.EditStatus,
                t.IsDefaultRow,
                t.DefaulRowNum,
                t.ControlRows,
                t.QCFlag
                ");
                strSql.Append("  FROM MSTB_QUA_CHECKITEMINFO t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
             
                return this.BaseRepository("DDit").FindList<MSTB_QUA_CHECKITEMINFOEntity>(strSql.ToString(), pagination);
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
        /// 获取表实体数据
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public MSTB_QUA_CHECKITEMINFOEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("DDit").FindEntity<MSTB_QUA_CHECKITEMINFOEntity>(keyValue);
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
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository("DDit").Delete<MSTB_QUA_CHECKITEMINFOEntity>(t=>t.ID.ToString() == keyValue);
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
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, MSTB_QUA_CHECKITEMINFOEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(Guid.Parse(keyValue));
                    this.BaseRepository("DDit").Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository("DDit").Insert(entity);
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

        /// <summary>
        /// 保存实体列表数据
        /// <summary>
        /// <param name="list">实体数据列表</param>
        /// <returns></returns>
        public void SaveList(List<MSTB_QUA_CHECKITEMINFOEntity> list)
        {
            var db = this.BaseRepository("DDit").BeginTrans();
            try
            {
                foreach(var item in list)                {
                    item.Modify(item.ID);
                    db.Update(item);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
