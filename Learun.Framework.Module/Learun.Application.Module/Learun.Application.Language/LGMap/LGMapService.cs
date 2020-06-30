using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Language
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public class LGMapService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public LGMapService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_TypeCode
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据结合（code,name）
        /// <param name="Code">语言包编码</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LanguageModel> GetDataList(string Code)
        {
            try
            {
                var sql = "SELECT F_Code as id,F_Name as name FROM LR_Lg_Map t WHERE F_TypeCode='" + Code + "' AND  t.F_Code <>'' AND t.F_Code<>'undefined'";
                return this.BaseRepository().FindList<LanguageModel>(sql);
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
        /// 获取列表数据
        /// <param name="TypeCode">编码</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByTypeCode(string TypeCode)
        {
            try
            {
                var sql = "select * from LR_Lg_Map where  F_TypeCode='" + TypeCode + "' AND  t.F_Code <>'' AND t.F_Code<>'undefined'";
                return this.BaseRepository().FindList<LGMapEntity>(sql);
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
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetList(string queryJson)
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
                strSql.Append(" FROM LR_Lg_Map t WHERE 1=1 AND  t.F_Code <>'' AND t.F_Code<>'undefined'");
                strSql.Append(" ORDER BY F_Code");
                return this.BaseRepository().FindList<LGMapEntity>(strSql.ToString());
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
        /// <param name="typeList">语言类型列表</param>
        /// <summary>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson, string typeList)
        {
            try
            {
                string[] list = typeList.Split(',');
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append("m.F_Code, m.F_Name as " + list[0] + " ");
                for (var i = 1; i < list.Length; i++)
                {
                    strSql.Append(", m" + i + ".F_Name as " + list[i] + " ");
                }
                strSql.Append(" FROM LR_Lg_Map m ");
                for (var j = 1; j < list.Length; j++)
                {
                    strSql.Append("LEFT JOIN LR_Lg_Map m" + j + " ON m.F_Code=m" + j + ".F_Code AND m" + j + ".F_TypeCode='" + list[j] + "' ");
                }
                strSql.Append(" WHERE m.F_TypeCode = '" + list[0] + "'  AND  m.F_Code <>'' AND m.F_Code<>'undefined'");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (queryParam.Property("keyword") != null && queryParam.Property("keyword").ToString() != "")
                {
                    dp.Add("F_Name", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                    strSql.Append(" AND m.F_Name like @F_Name ");
                }
                return this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
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
        public LGMapEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LGMapEntity>(keyValue);
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
        /// 根据名称获取列表
        /// <param name="keyValue">F_Name</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByName(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Lg_Map t WHERE 1=1 AND  t.F_Code <>'' AND t.F_Code<>'undefined' AND t.F_Name='" + keyValue + "'");
                return this.BaseRepository().FindList<LGMapEntity>(strSql.ToString());
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
        /// 根据名称与类型获取列表
        /// <param name="keyValue">F_Name</param>
        /// <param name="typeCode">typeCode</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByNameAndType(string keyValue, string typeCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Lg_Map t WHERE 1=1 AND  t.F_Code <>'' AND t.F_Code<>'undefined' AND t.F_Name='" + keyValue + "' AND t.F_TypeCode='" + typeCode + "'");
                return this.BaseRepository().FindList<LGMapEntity>(strSql.ToString());
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
                this.BaseRepository().Delete<LGMapEntity>(t => t.F_Code == keyValue);
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
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">F_Code</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string nameList, string newNameList, string code)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(code))
                {//编辑
                    JObject jo = JObject.Parse(nameList);
                    IEnumerable<JProperty> properties = jo.Properties();
                    //将原列表存入字典
                    var dic = new Dictionary<string, string>();
                    foreach (JProperty item in properties)
                    {
                        dic.Add(item.Name, item.Value.ToString());
                    }

                    //新列表
                    JObject newjo = JObject.Parse(newNameList);
                    IEnumerable<JProperty> newproperties = newjo.Properties();
                    foreach (JProperty item in newproperties)
                    {
                        if (dic.ContainsKey(item.Name) && (item.Value.ToString() != dic[item.Name]))
                        {
                            var sql = "UPDATE LR_LG_MAP SET F_Name=@newName WHERE F_Name=@oldName AND F_Code=@code";
                            db.ExecuteBySql(sql, new { newName = item.Value.ToString(), oldName = dic[item.Name], code = code });
                        }
                        else if (!dic.ContainsKey(item.Name))
                        {
                            LGMapEntity entity = new LGMapEntity();
                            entity.Create();
                            entity.F_Name = item.Value.ToString();
                            entity.F_TypeCode = item.Name;
                            entity.F_Code = code;
                            db.Insert<LGMapEntity>(entity);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {//新增
                    //转为可遍历对象
                    JObject jo = JObject.Parse(newNameList);
                    IEnumerable<JProperty> properties = jo.Properties();
                    var F_Code = Guid.NewGuid().ToString();
                    //新增每一项
                    foreach (JProperty item in properties)
                    {
                        LGMapEntity entity = new LGMapEntity();
                        entity.Create();
                        entity.F_Name = item.Value.ToString();
                        entity.F_TypeCode = item.Name;
                        entity.F_Code = F_Code;
                        db.Insert<LGMapEntity>(entity);
                    }
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
