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
    /// 日 期：2019-01-09 10:31
    /// 描 述：安利查询
    /// </summary>
    public class AnlySeleService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取报表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT  ");
                strSql.Append(@"
                t.id,
                t.area,
                t.item_number,
                t.version,
                t.name,
                t.remark,
                t.code,
                t.creater,
                t.aprver,
                t.status,
                t.effectivedate,
                t.isaprv,
                t.cyc,
                t.recordstatus,
                t.recordlasteditdt,
                t.recordguid,
                t.filename
                ");
                strSql.Append("  FROM (SELECT * FROM DDit.DBO.MSTB_QUA_PRODUCTRECORD WHERE Name like N'纽崔莱%')t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["area"].IsEmpty())
                {
                    dp.Add("area", "%" + queryParam["area"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.area Like @area ");
                }
                if (!queryParam["item_number"].IsEmpty())
                {
                    dp.Add("item_number", "%" + queryParam["item_number"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.item_number Like @item_number ");
                }
                if (!queryParam["name"].IsEmpty())
                {
                    dp.Add("name", "%" + queryParam["name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.name Like @name ");
                }
                return this.BaseRepository("DDit").FindTable(strSql.ToString(),dp);
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

    }
}
        #endregion 

