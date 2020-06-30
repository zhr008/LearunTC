using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Report
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 15:17
    /// 描 述：报表文件管理
    /// </summary>
    public class RptManageService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_RPT_FileInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_Code,
                t.F_Name,
                t.F_Type,
                t.F_SortCode,
                f.F_FileName as F_File,
                t.F_Description
                ");
                strSql.Append("  FROM LR_RPT_FileInfo t INNER JOIN LR_Base_AnnexesFile f on t.F_File=f.F_FolderId ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_Code"].IsEmpty())
                {
                    dp.Add("F_Code", "%" + queryParam["F_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Code Like @F_Code ");
                }
                if (!queryParam["F_Name"].IsEmpty())
                {
                    dp.Add("F_Name", "%" + queryParam["F_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Name Like @F_Name ");
                }
                if (!queryParam["F_Type"].IsEmpty())
                {
                    dp.Add("F_Type", queryParam["F_Type"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Type = @F_Type ");
                }
                this.BaseRepository().FindTable("select * from tb");
                return this.BaseRepository().FindList<LR_RPT_FileInfoEntity>(strSql.ToString(), dp, pagination);
               
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_RPT_FileInfoEntity> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"
                t.F_Id,
                t.F_Code,
                t.F_Name,
                t.F_Type,
                t.F_SortCode,
                f.F_FileName as F_File,
                t.F_Description
                ");
            strSql.Append("  FROM LR_RPT_FileInfo t INNER JOIN LR_Base_AnnexesFile f on t.F_File=f.F_FolderId ");
            return this.BaseRepository().FindList<LR_RPT_FileInfoEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取LR_RPT_FileInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_RPT_FileInfoEntity GetLR_RPT_FileInfoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_RPT_FileInfoEntity>(keyValue);
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
                this.BaseRepository().Delete<LR_RPT_FileInfoEntity>(t => t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_RPT_FileInfoEntity entity)
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
