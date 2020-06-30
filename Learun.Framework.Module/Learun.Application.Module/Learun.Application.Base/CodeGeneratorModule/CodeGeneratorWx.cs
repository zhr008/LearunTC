using Learun.Application.Base.SystemModule;
using Learun.Application.BaseModule.CodeGeneratorModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Linq;

namespace Learun.Application.Base.CodeGeneratorModule
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2020.02.17
    /// 描 述：代码生成器类（小程序端）
    /// </summary>
    public class CodeGeneratorWx
    {
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();
        private DatabaseTableIBLL databaseTableIBLL = new DatabaseTableBLL();

        #region 通用方法
        /// <summary>
        /// 注释头
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <returns></returns>
        private string NotesCreate(BaseModel baseInfo)
        {
            UserInfo userInfo = LoginUserInfo.Get();

            StringBuilder sb = new StringBuilder();
            sb.Append("    /// <summary>\r\n");
            sb.Append("    \r\n");
            sb.Append("    \r\n");
            sb.Append("    /// 创 建：" + userInfo.realName + "\r\n");
            sb.Append("    /// 日 期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
            sb.Append("    /// 描 述：" + baseInfo.describe + "\r\n");
            sb.Append("    /// </summary>\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 后端区域名
        /// </summary>
        /// <param name="strBackArea">后端区域</param>
        /// <returns></returns>
        private string getBackArea(string strBackArea)
        {
            if (string.IsNullOrEmpty(strBackArea))
            {
                return "";
            }
            else
            {
                return "." + strBackArea;
            }
        }
        #endregion

        #region 实体类
        /// <summary>
        /// 实体类创建(自定义开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">数据表</param>
        /// <param name="pkey">主键</param>
        /// <param name="baseInfo">基础信息</param>
        /// <param name="colDataObj">列信息</param>
        /// <param name="isMain">是否是主表</param>
        /// <returns></returns>
        public string EntityCreate(string databaseLinkId, string tableName, string pkey, BaseModel baseInfo, ColModel colDataObj, bool isMain)
        {

            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();


                StringBuilder sb = new StringBuilder();

                string pkDataType = "";
                string pkName = "";

                string pk = "";
                string createUserId = "";
                string createUserName = "";
                string createDate = "";
                string modifyUserId = "";
                string modifyUserName = "";
                string modifyDate = "";

                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");



                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + tableName + "Entity \r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                Dictionary<string, string> fieldMap = new Dictionary<string, string>();

                #region 设置字段根据数据库字段
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, tableName);
                foreach (var field in fieldList)
                {
                    fieldMap.Add(field.f_column, field.f_column);

                    string datatype = databaseTableIBLL.FindModelsType(field.f_datatype);

                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + field.f_remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    if (field.f_key == "1" && (datatype == "int?" || datatype == "decimal?"))// 考虑到自增量
                    {
                        sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                    sb.Append("        [Column(\"" + field.f_column.ToUpper() + "\")]\r\n");
                    sb.Append("        public " + datatype + " " + field.f_column + " { get; set; }\r\n");

                    #region 创建时间和修改时间
                    if (field.f_column == pkey)
                    {
                        if (datatype == "string")
                        {
                            pk = "            this." + field.f_column + " = Guid.NewGuid().ToString();\r\n";
                        }
                        pkDataType = datatype;
                        pkName = field.f_column;
                    }
                    if (field.f_column == "F_CreateUserId")
                    {
                        createUserId = "            this.F_CreateUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_CreateUserName")
                    {
                        createUserName = "            this.F_CreateUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_CreateDate")
                    {
                        createDate = "            this.F_CreateDate = DateTime.Now;\r\n";
                    }

                    if (field.f_column == "F_ModifyUserId")
                    {
                        modifyUserId = "            this.F_ModifyUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_ModifyUserName")
                    {
                        modifyUserName = "            this.F_ModifyUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_ModifyDate")
                    {
                        modifyDate = "            this.F_ModifyDate = DateTime.Now;\r\n";
                    }
                    #endregion
                }
                #endregion

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public void Create(UserInfo userInfo)\r\n");
                sb.Append("      {\r\n");
                sb.Append(pk);
                sb.Append(createDate);

                sb.Append(createUserId);
                sb.Append(createUserName);
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public void Modify(" + pkDataType + " keyValue, UserInfo userInfo)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            this." + pkName + " = keyValue;\r\n");
                sb.Append(modifyDate);
                sb.Append(modifyUserId);
                sb.Append(modifyUserName);
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");

                // 如果是主表需要增加额外字段
                if (isMain)
                {
                    sb.Append("        #region 扩展字段\r\n");

                    foreach (var col in colDataObj.fields)
                    {
                        if (!fieldMap.ContainsKey(col.field))
                        {
                            sb.Append("        /// <summary>\r\n");
                            sb.Append("        /// " + col.fieldName + "\r\n");
                            sb.Append("        /// </summary>\r\n");
                            sb.Append("        [NotMapped]\r\n");
                            sb.Append("        public string " + col.field + " { get; set; }\r\n");
                        }
                    }

                    sb.Append("        #endregion\r\n");
                }


                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 映射类
        /// <summary>
        /// 创建实体映射类（EF必须）(自定义开发模板)
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="pkey">主键</param>
        /// <param name="baseInfo">基础信息</param>
        /// <returns></returns>
        public string MappingCreate(string tableName, string pkey, BaseModel baseInfo)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

                sb.Append("namespace  Learun.Application.Mapping\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + tableName + "Map : EntityTypeConfiguration<" + tableName + "Entity>\r\n");
                sb.Append("    {\r\n");
                sb.Append("        public " + tableName + "Map()\r\n");
                sb.Append("        {\r\n");
                sb.Append("            #region 表、主键\r\n");
                sb.Append("            //表\r\n");
                sb.Append("            this.ToTable(\"" + tableName.ToUpper() + "\");\r\n");
                sb.Append("            //主键\r\n");
                sb.Append("            this.HasKey(t => t." + pkey + ");\r\n");
                sb.Append("            #endregion\r\n\r\n");

                sb.Append("            #region 配置关系\r\n");
                sb.Append("            #endregion\r\n");
                sb.Append("        }\r\n");
                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 获取服务类函数体字串
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <returns></returns>
        private string getServiceTry(string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowServiceException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 获取服务类函数体字串(事务)
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <param name="dbname">数据库名称</param>
        /// <returns></returns>
        private string getTransServiceTry(string content, string dbname)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            var db = this.BaseRepository(" + dbname + ").BeginTrans();\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                db.Rollback();\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowServiceException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 设置左关联递归表
        /// </summary>
        /// <param name="queryDbTableMap">需要查询的表数据</param>
        /// <param name="queryDbTableIndex">需要查询的表数据（下标）</param>
        /// <param name="dbTableMap">所有表数据</param>
        /// <param name="tableName">表名</param>
        /// <param name="mainTable">主表</param>
        private void SetLeftTable(Dictionary<string, DbTableModel> queryDbTableMap, Dictionary<string, int> queryDbTableIndex, Dictionary<string, DbTableModel> dbTableMap, string tableName, string mainTable)
        {
            if (!string.IsNullOrEmpty(tableName) && !queryDbTableMap.ContainsKey(tableName))
            {
                queryDbTableMap.Add(tableName, dbTableMap[tableName]);
                queryDbTableIndex.Add(tableName, queryDbTableMap.Count);
                if (!string.IsNullOrEmpty(dbTableMap[tableName].relationName) && dbTableMap[tableName].relationName != mainTable)
                {
                    SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, dbTableMap[tableName].relationName, mainTable);
                }
            }
        }
        /// <summary>
        /// 获取删除数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="pDbTable">父级表数据</param>
        /// <param name="mainTable">主表名称</param>
        private string DeleteToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, DbTableModel pDbTable, string mainTable)
        {
            string content = "";
            foreach (var tree in TableTree)
            {
                if (tree.ChildNodes.Count > 0)
                {
                    if (tree.parentId == "0" || pDbTable == null)
                    {
                        content += "                var " + Str.FirstLower(tree.data.name) + "Entity = Get" + tree.data.name + "Entity(keyValue); \r\n";
                    }
                    else
                    {
                        content += "                var " + Str.FirstLower(tree.data.name) + "Entity = Get" + tree.data.name + "Entity(" + Str.FirstLower(pDbTable.name) + "Entity." + tree.data.relationField + "); \r\n";
                    }
                }
                content += DeleteToSelectSql(tree.ChildNodes, tree.data, mainTable);
            }
            return content;
        }
        /// <summary>
        /// 获取更新数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="pDbTable">父级表数据</param>
        /// <param name="mainTable">主表名称</param>
        private string UpdateToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, DbTableModel pDbTable, string mainTable)
        {
            string content = "";
            foreach (var tree in TableTree)
            {
                if (tree.ChildNodes.Count > 0)
                {
                    if (tree.parentId == "0" || pDbTable == null)
                    {
                        content += "                    var " + Str.FirstLower(tree.data.name) + "EntityTmp = Get" + tree.data.name + "Entity(keyValue); \r\n";
                    }
                    else
                    {
                        content += "                    var " + Str.FirstLower(tree.data.name) + "EntityTmp = Get" + tree.data.name + "Entity(" + Str.FirstLower(pDbTable.name) + "Entity." + tree.data.relationField + "); \r\n";
                    }
                }
                content += UpdateToSelectSql(tree.ChildNodes, tree.data, mainTable);
            }
            return content;
        }
        private Dictionary<string, string> InsertGuidMap = new Dictionary<string, string>();
        /// <summary>
        /// 获取新增数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="content">拼接代码内容</param>
        /// <param name="mainTable">主表名称</param>
        /// <param name="mainPk">主表主键</param>
        private void InsertToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, string content, string mainTable, string mainPk)
        {
            foreach (var tree in TableTree)
            {
                if (!string.IsNullOrEmpty(tree.data.relationName))
                {
                    if (tree.data.relationName != mainTable || tree.data.relationField != mainPk)
                    {
                        if (!InsertGuidMap.ContainsKey(tree.data.relationName + "|" + tree.data.relationField))
                        {
                            InsertGuidMap.Add(tree.data.relationName + "|" + tree.data.relationField, "1");
                            content += "                var " + Str.FirstLower(tree.data.relationName) + "Entity." + tree.data.relationField + " = Guid.NewGuid().ToString(); \r\n";
                        }
                    }
                }
                InsertToSelectSql(tree.ChildNodes, content, mainTable, mainPk);
            }
        }
        /// <summary>
        /// 服务类创建(移动开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接地址主键</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ServiceCreate(string databaseLinkId, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData, BaseModel baseInfo)
        {
            try
            {
                #region 添加数据库配置
                string dbname = "";
                if (databaseLinkId != "systemdb")
                {
                    DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                    string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                    if (connectionString != dbEntity.F_DbConnection)
                    {
                        if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                        {
                            string providerName = "System.Data.SqlClient";
                            if (dbEntity.F_DbType == "MySql")
                            {
                                providerName = "MySql.Data.MySqlClient";
                            }
                            else if (dbEntity.F_DbType == "Oracle")
                            {
                                providerName = "Oracle.ManagedDataAccess.Client";
                            }
                            Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                        }
                        dbname = "\"" + dbEntity.F_DBName + "\"";
                    }
                }
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                // 列表数据
                string querySqlField = "                t." + mainPkey;                                         // 查询数据字段
                Dictionary<string, DbTableModel> queryDbTableMap = new Dictionary<string, DbTableModel>();      // 需要查询的表
                Dictionary<string, int> queryDbTableIndex = new Dictionary<string, int>();
                foreach (var col in colData.fields)
                {
                    string tableName = compontMap[col.id].table;

                    if (querySqlField != "")
                    {
                        querySqlField += ",\r\n";
                    }

                    if (tableName == mainTable)
                    {
                        querySqlField += "                t." + col.field;
                    }
                    else
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                        querySqlField += "                t" + queryDbTableIndex[tableName].ToString() + "." + col.field;
                    }
                }
                if (string.IsNullOrEmpty(querySqlField))
                {
                    IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, mainTable);
                    foreach (var field in fieldList)
                    {
                        if (querySqlField != "")
                        {
                            querySqlField += ",\r\n";
                        }
                        querySqlField += "                t." + field.f_column;
                    }
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 数据查询
                // 查询条件数据
                foreach (var queryFiled in queryData.fields)
                {
                    string tableName = compontMap[queryFiled.id].table;
                    if (tableName != mainTable)
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                    }
                }

                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");

                // 获取列表数据(分页)
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表分页数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");

                string content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM " + mainTable + " t \");\r\n";

                foreach (var key in queryDbTableMap.Keys)
                {
                    string ct = "t" + queryDbTableIndex[key].ToString();
                    string pt = "t";
                    if (queryDbTableMap[key].relationName != mainTable)
                    {
                        pt = "t" + queryDbTableIndex[key].ToString();
                    }
                    content += "                strSql.Append(\"  LEFT JOIN " + queryDbTableMap[key].name + " " + ct + " ON " + ct + "." + queryDbTableMap[key].field + " = " + pt + "." + queryDbTableMap[key].relationField + " \");\r\n";
                }
                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }
                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + compontMap[queryFiled.id].field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    if (compontMap[queryFiled.id].type == "text" || compontMap[queryFiled.id].type == "textarea" || compontMap[queryFiled.id].type == "texteditor" || compontMap[queryFiled.id].type == "encode")
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\", \"%\" + queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString() + \"%\", DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                    }
                    else
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\",queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString(), DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";
                        }
                    }
                    content += "                }\r\n";
                }

                if (colData.isTree == "1")
                {
                    content += "                if (!queryParam[\"" + colData.treefieldRe + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"" + colData.treefieldRe + "\",queryParam[\"" + colData.treefieldRe + "\"].ToString(), DbType.String);\r\n";
                    content += "                    strSql.Append(\" AND t." + colData.treefieldRe + " = @" + colData.treefieldRe + " \");\r\n";
                    content += "                }\r\n";
                }

                content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp, pagination);\r\n";
                sb.Append(getServiceTry(content));

                // 获取列表数据(不分页)
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");

                content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM " + mainTable + " t \");\r\n";

                foreach (var key in queryDbTableMap.Keys)
                {
                    string ct = "t" + queryDbTableIndex[key].ToString();
                    string pt = "t";
                    if (queryDbTableMap[key].relationName != mainTable)
                    {
                        pt = "t" + queryDbTableIndex[key].ToString();
                    }
                    content += "                strSql.Append(\"  LEFT JOIN " + queryDbTableMap[key].name + " " + ct + " ON " + ct + "." + queryDbTableMap[key].field + " = " + pt + "." + queryDbTableMap[key].relationField + " \");\r\n";
                }
                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }
                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + compontMap[queryFiled.id].field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    if (compontMap[queryFiled.id].type == "text" || compontMap[queryFiled.id].type == "textarea" || compontMap[queryFiled.id].type == "texteditor" || compontMap[queryFiled.id].type == "encode")
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\", \"%\" + queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString() + \"%\", DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                    }
                    else
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\",queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString(), DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";
                        }
                    }
                    content += "                }\r\n";
                }
                if (colData.isTree == "1")
                {
                    content += "                if (!queryParam[\"" + colData.treefieldRe + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"" + colData.treefieldRe + "\",queryParam[\"" + colData.treefieldRe + "\"].ToString(), DbType.String);\r\n";
                    content += "                    strSql.Append(\" AND t." + colData.treefieldRe + " = @" + colData.treefieldRe + " \");\r\n";
                    content += "                }\r\n";
                }
                content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp);\r\n";
                sb.Append(getServiceTry(content));

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue );\r\n";
                    sb.Append(getServiceTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    if (tableOne.name == mainTable)
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(keyValue);\r\n";
                    }
                    else
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue);\r\n";
                    }
                    sb.Append(getServiceTry(content));
                }

                // 获取树形数据列表
                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    content = "";
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取树形数据\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public DataTable GetSqlTree()\r\n");
                    content = "                return this.BaseRepository().FindTable(\" " + colData.treeSql + " \");\r\n";
                    sb.Append(getServiceTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                if (dbTableList.Count == 1)
                {
                    content += "                this.BaseRepository(" + dbname + ").Delete<" + mainTable + "Entity>(t=>t." + mainPkey + " == keyValue);\r\n";
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    content += DeleteToSelectSql(TableTree, null, mainTable);
                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)// 关联的表不是主表
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "Entity." + tableOne.relationField + ");\r\n";
                        }
                        else
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.pk + " == keyValue);\r\n";
                        }
                    }
                    content += "                db.Commit();\r\n";
                    sb.Append(getTransServiceTry(content, dbname));
                }

                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = " UserInfo userInfo, string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                if (!string.IsNullOrEmpty(keyValue))\r\n";
                content += "                {\r\n";

                // 更新
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Modify(keyValue,userInfo);\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Update(entity);\r\n";

                }
                else
                {
                    content += UpdateToSelectSql(TableTree, null, mainTable);
                    content += "                    entity.Modify(keyValue,userInfo);\r\n";
                    content += "                    db.Update(entity);\r\n";

                    foreach (var tableOne in dbTableList)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";
                            // 如果是编辑表格数据
                            content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                            content += "                    {\r\n";
                            content += "                        item.Create(userInfo);\r\n";
                            content += "                        item." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                        db.Insert(item);\r\n";
                            content += "                    }\r\n";
                        }
                        else if (tableOne.name != mainTable)// 不是
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";

                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create(userInfo);\r\n";
                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                        }
                    }
                }


                content += "                }\r\n";
                content += "                else\r\n";
                content += "                {\r\n";

                // 新增
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Create(userInfo);\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Insert(entity);\r\n";
                }
                else
                {
                    content += "                    entity.Create(userInfo);\r\n";
                    content += "                    db.Insert(entity);\r\n";

                    InsertToSelectSql(TableTree, content, mainTable, mainPkey);

                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)
                        {
                            string entityName = Str.FirstLower(tableOne.relationName) + "Entity.";
                            if (tableOne.relationName == mainTable)
                            {
                                entityName = "entity.";
                            }

                            if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                            {
                                // 如果是编辑表格数据
                                content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                                content += "                    {\r\n";
                                content += "                        item.Create(userInfo);\r\n";
                                content += "                        item." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                        db.Insert(item);\r\n";
                                content += "                    }\r\n";
                            }
                            else if (tableOne.name != mainTable)// 不是
                            {
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create(userInfo);\r\n";
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                            }
                        }
                    }


                }
                content += "                }\r\n";

                if (dbTableList.Count > 1)
                {
                    content += "                db.Commit();\r\n";
                }


                if (dbTableList.Count == 1)
                {
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    sb.Append(getTransServiceTry(content, dbname));
                }

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 业务类
        /// <summary>
        /// 获取服务类函数体字串
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <returns></returns>
        private string getBllTry(string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowBusinessException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 业务类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string BllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "BLL : " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "Service " + Str.FirstLower(baseInfo.name) + "Service = new " + baseInfo.name + "Service();\r\n\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");

                // 获取列表数据（分页）
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表分页数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                string content = "";
                sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetPageList(pagination, queryJson);\r\n";
                sb.Append(getBllTry(content));

                // 获取列表数据（不分页）
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                content = "";
                sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");
                content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetList(queryJson);\r\n";
                sb.Append(getBllTry(content));



                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "List(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "Entity(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("         public List<TreeModel> GetTree()\r\n");
                    content = "";
                    content += "                DataTable list = " + Str.FirstLower(baseInfo.name) + "Service.GetSqlTree();\r\n";
                    content += "                List<TreeModel> treeList = new List<TreeModel>();\r\n";
                    content += "                foreach (DataRow item in list.Rows)\r\n";
                    content += "                {\r\n";
                    content += "                    TreeModel node = new TreeModel\r\n";
                    content += "                    {\r\n";
                    content += "                        id = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        text = item[\"" + colData.treefieldShow + "\"].ToString(),\r\n";
                    content += "                        value = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        showcheck = false,\r\n";
                    content += "                        checkstate = 0,\r\n";
                    content += "                        isexpand = true,\r\n";
                    content += "                        parentId = item[\"" + colData.treeParentId + "\"].ToString()\r\n";
                    content += "                    };\r\n";
                    content += "                    treeList.Add(node);";
                    content += "                }\r\n";
                    content += "                return treeList.ToTree();\r\n";
                    sb.Append(getBllTry(content));
                }

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.DeleteEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));


                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "UserInfo userInfo, string keyValue, " + mainTable + "Entity entity,";
                string paramStr2 = "userInfo, keyValue, entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.SaveEntity(" + paramStr2 + ");\r\n";
                sb.Append(getBllTry(content));

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 业务接口类
        /// <summary>
        /// 业务接口类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string IBllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public interface " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据（分页）
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表分页数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">查询参数</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson);\r\n");

                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        IEnumerable<" + mainTable + "Entity> GetList(string queryJson);\r\n");


                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue);\r\n");
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue);\r\n");
                }

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        List<TreeModel> GetTree();\r\n");
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void DeleteEntity(string keyValue);\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "UserInfo userInfo, string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        void SaveEntity(" + paramStr + ");\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 控制器类
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="TableTree"></param>
        /// <param name="girdDbTableList"></param>
        /// <param name="baseInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetEntityCode(List<TreeModelEx<DbTableModel>> TableTree, List<DbTableModel> girdDbTableList, BaseModel baseInfo, string tableName)
        {
            string res = "";
            foreach (var tableOne in TableTree)
            {

                string keyvalue = "keyValue";

                if (!string.IsNullOrEmpty(tableName))
                {
                    keyvalue = tableOne.data.relationName + "Data." + tableOne.data.relationField;
                }

                if (girdDbTableList.FindAll(t => t.name == tableOne.data.name).Count > 0)
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "List( " + keyvalue + " );\r\n";
                }
                else
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "Entity( " + keyvalue + " );\r\n";
                }

                if (tableOne.ChildNodes.Count > 0)
                {
                    res += GetEntityCode(tableOne.ChildNodes, girdDbTableList, baseInfo, tableOne.data.name);
                }

            }
            return res;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="TableTree"></param>
        /// <param name="baseInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetEntityCode(List<TreeModelEx<DbTableModel>> TableTree, BaseModel baseInfo, string tableName)
        {
            string res = "";
            foreach (var tableOne in TableTree)
            {

                string keyvalue = "keyValue";

                if (!string.IsNullOrEmpty(tableName))
                {
                    keyvalue = tableOne.data.relationName + "Data." + tableOne.data.relationField;
                }

                res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "Entity( " + keyvalue + " );\r\n";

                if (tableOne.ChildNodes.Count > 0)
                {
                    res += GetEntityCode(tableOne.ChildNodes, baseInfo, tableOne.data.name);
                }

            }
            return res;
        }
        /// <summary>
        /// 控制器类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string ControllerCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Web.Mvc;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseInfo.outputArea + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表分页数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetPageList(string pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            Pagination paginationobj = pagination.ToObject<Pagination>();\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetPageList(paginationobj, queryJson);\r\n");
                sb.Append("            var jsonData = new\r\n");
                sb.Append("            {\r\n");
                sb.Append("                rows = data,\r\n");
                sb.Append("                total = paginationobj.total,\r\n");
                sb.Append("                page = paginationobj.page,\r\n");
                sb.Append("                records = paginationobj.records\r\n");
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetList(string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                sb.Append("            return Success(data);\r\n");
                sb.Append("        }\r\n");


                // 获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetFormData(string keyValue)\r\n");
                sb.Append("        {\r\n");
                string strEntityCode = GetEntityCode(TableTree, girdDbTableList, baseInfo, "");
                sb.Append(strEntityCode);
                sb.Append("            var jsonData = new {\r\n");
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("                " + tableOne.name + " = " + tableOne.name + "Data,\r\n");
                }
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    // 获取左侧树形数据
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        [HttpGet]\r\n");
                    sb.Append("        [AjaxOnly]\r\n");
                    sb.Append("        public ActionResult GetTree()\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetTree();\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult DeleteForm(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [ValidateAntiForgeryToken]\r\n");
                sb.Append("        [AjaxOnly]\r\n");


                // 函数传入参数
                string paramStr = "string keyValue, string strEntity,";
                string paramStr2 = "userInfo,keyValue,entity,";
                string paramStr3 = "            " + mainTable + "Entity entity = strEntity.ToObject<" + mainTable + "Entity>();\r\n";

                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr3 += "            " + tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity = str" + Str.FirstLower(tableOne.name) + "Entity.ToObject<" + tableOne.name + "Entity>();\r\n";
                        }
                        else
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                            paramStr3 += "            List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List = str" + Str.FirstLower(tableOne.name) + "List.ToObject<List<" + tableOne.name + "Entity>>();\r\n";

                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public ActionResult SaveForm(" + paramStr + ")\r\n");
                sb.Append("        {\r\n");

                sb.Append("            UserInfo userInfo = LoginUserInfo.Get();");

                sb.Append(paramStr3);
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveEntity(" + paramStr2 + ");\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 页面类
        /// <summary>
        /// 业务类创建(移动开发模板) 
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="compontMap"></param>
        /// <param name="queryData"></param>
        /// <param name="colData"></param>
        /// <returns></returns>
        public string IndexCreate(BaseModel baseInfo, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
                sb.Append("}\r\n");

                sb.Append("<div class=\"lr-layout " + (colData.isTree == "1" ? "lr-layout-left-center\"  id=\"lr_layout\" " : "\"") + "  >\r\n");
                if (colData.isTree == "1")
                {
                    sb.Append("    <div class=\"lr-layout-left\">\r\n");
                    sb.Append("        <div class=\"lr-layout-wrap\">\r\n");
                    sb.Append("            <div class=\"lr-layout-title lrlg \">树形列表</div>\r\n");
                    sb.Append("            <div id=\"dataTree\" class=\"lr-layout-body\"></div>\r\n");
                    sb.Append("        </div>\r\n");
                    sb.Append("    </div>\r\n");
                }

                sb.Append("    <div class=\"lr-layout-center\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap " + (colData.isTree == "1" ? "" : "lr-layout-wrap-notitle") + " \">\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            <div class=\"lr-layout-title\">\r\n");
                    sb.Append("                <span id=\"titleinfo\" class=\"lrlg\">列表信息</span>\r\n");
                    sb.Append("            </div>\r\n");
                }

                sb.Append("            <div class=\"lr-layout-tool\">\r\n");
                sb.Append("                <div class=\"lr-layout-tool-left\">\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"datesearch\"></div>\r\n");
                    sb.Append("                    </div>\r\n");
                }
                if (queryData.fields.Count > 0)
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"multiple_condition_query\">\r\n");
                    sb.Append("                            <div class=\"lr-query-formcontent\">\r\n");
                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        sb.Append("                                <div class=\"col-xs-" + (12 / Convert.ToInt32(item.portion)) + " lr-form-item\">\r\n");
                        sb.Append("                                    <div class=\"lr-form-item-title\">" + compont.title + "</div>\r\n");

                        switch (compont.type)
                        {
                            case "text":
                            case "textarea":
                            case "datetimerange":
                            case "texteditor":
                            case "encode":
                                sb.Append("                                    <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control\" />\r\n");
                                break;
                            case "radio":
                            case "checkbox":
                            case "select":
                            case "organize":
                            case "currentInfo":
                                sb.Append("                                    <div id=\"" + compont.field + "\"></div>\r\n");
                                break;
                        }
                        sb.Append("                                </div>\r\n");
                    }
                    sb.Append("                            </div>\r\n");
                    sb.Append("                        </div>\r\n");
                    sb.Append("                    </div>\r\n");
                }

                sb.Append("                </div>\r\n");
                sb.Append("                <div class=\"lr-layout-tool-right\">\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\">\r\n");
                sb.Append("                        <a id=\"lr_refresh\" class=\"btn btn-default\"><i class=\"fa fa-refresh\"></i></a>\r\n");
                sb.Append("                    </div>\r\n");

                if (colData.btns.Length > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");

                    foreach (string btn in colData.btns)
                    {
                        switch (btn)
                        {
                            case "add":
                                sb.Append("                        <a id=\"lr_add\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>\r\n");
                                break;
                            case "edit":
                                sb.Append("                        <a id=\"lr_edit\"  class=\"btn btn-default\"><i class=\"fa fa-pencil-square-o\"></i>&nbsp;编辑</a>\r\n");
                                break;
                            case "delete":
                                sb.Append("                        <a id=\"lr_delete\" class=\"btn btn-default\"><i class=\"fa fa-trash-o\"></i>&nbsp;删除</a>\r\n");
                                break;
                            case "print":
                                sb.Append("                        <a id=\"lr_print\"   class=\"btn btn-default\"><i class=\"fa fa-print\"></i>&nbsp;打印</a>\r\n");
                                break;
                        }
                    }
                    sb.Append("                    </div>\r\n");
                }
                if (colData.btnexs.Count > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");
                    foreach (var btn in colData.btnexs)
                    {
                        sb.Append("                        <a id=\"" + btn.id + "\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;" + btn.name + "</a>\r\n");
                    }
                    sb.Append("                    </div>\r\n");
                }


                sb.Append("                </div>\r\n");
                sb.Append("            </div>\r\n");
                sb.Append("            <div class=\"lr-layout-body\" id=\"gridtable\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Index.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 页面js类
        /// <summary>
        /// 业务JS类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTableList"></param>
        /// <param name="compontMap"></param>
        /// <param name="colData"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string IndexJSCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }


                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2020 力软信息技术（苏州）有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");

                if (queryData.isDate == "1")
                {
                    sb.Append("    var startTime;\r\n");
                    sb.Append("    var endTime;\r\n");
                }

                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            // 初始化左侧树形数据\r\n");
                    sb.Append("            $('#dataTree').lrtree({\r\n");
                    if (colData.treeSource == "1")
                    {// 数据源获取
                        sb.Append("                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=" + colData.treeSourceId + "&parentId=" + colData.treeParentId + "&Id=" + colData.treefieldId + "&showId=" + colData.treefieldShow + "',\r\n");
                    }
                    else
                    {// sql语句的
                        sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetTree',\r\n");
                    }
                    sb.Append("                nodeClick: function (item) {\r\n");
                    sb.Append("                    page.search({ " + colData.treefieldRe + ": item.value });\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.isDate == "1")
                {
                    sb.Append("            // 时间搜索框\r\n");
                    sb.Append("            $('#datesearch').lrdate({\r\n");
                    sb.Append("                dfdata: [\r\n");
                    sb.Append("                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }\r\n");
                    sb.Append("                ],\r\n");
                    sb.Append("                // 月\r\n");
                    sb.Append("                mShow: false,\r\n");
                    sb.Append("                premShow: false,\r\n");
                    sb.Append("                // 季度\r\n");
                    sb.Append("                jShow: false,\r\n");
                    sb.Append("                prejShow: false,\r\n");
                    sb.Append("                // 年\r\n");
                    sb.Append("                ysShow: false,\r\n");
                    sb.Append("                yxShow: false,\r\n");
                    sb.Append("                preyShow: false,\r\n");
                    sb.Append("                yShow: false,\r\n");
                    sb.Append("                // 默认\r\n");
                    sb.Append("                dfvalue: '1',\r\n");
                    sb.Append("                selectfn: function (begin, end) {\r\n");
                    sb.Append("                    startTime = begin;\r\n");
                    sb.Append("                    endTime = end;\r\n");
                    sb.Append("                    page.search();\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.fields.Count > 0)
                {
                    sb.Append("            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {\r\n");
                    sb.Append("                page.search(queryJson);\r\n");
                    sb.Append("            }, " + (queryData.height > 0 ? queryData.height : 220) + ", " + (queryData.width > 0 ? queryData.width : 400) + ");\r\n");

                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        if (compont.type == "select")
                        {
                            if (compont.dataSource == "0")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                            }
                        }
                        else if (compont.type == "organize" || compont.type == "currentInfo")
                        {
                            if (compont.dataType == "company")
                            {
                                sb.Append("            $('#" + compont.field + "').lrCompanySelect();\r\n");
                            }
                            else if (compont.dataType == "department")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDepartmentSelect();\r\n");
                            }
                            else if (compont.dataType == "user")
                            {
                                sb.Append("            $('#" + compont.field + "').lrUserSelect(0);\r\n");
                            }
                        }
                        else if (compont.type == "radio" || compont.type == "checkbox")
                        {

                            sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                            sb.Append("                type: '" + compont.type + "',\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                code: '" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                dataType: 'dataSource',\r\n");
                                sb.Append("                code: '" + vlist[0] + "',\r\n");
                                sb.Append("                value: '" + vlist[2] + "',\r\n");
                                sb.Append("                text: '" + vlist[1] + "',\r\n");
                            }
                            sb.Append("            });\r\n");
                        }
                    }
                }

                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");

                foreach (var btn in colData.btns)
                {
                    switch (btn)
                    {
                        case "add":
                            sb.Append("            // 新增\r\n");
                            sb.Append("            $('#lr_add').on('click', function () {\r\n");
                            sb.Append("                learun.layerForm({\r\n");
                            sb.Append("                    id: 'form',\r\n");
                            sb.Append("                    title: '新增',\r\n");
                            sb.Append("                    url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form',\r\n");
                            sb.Append("                    width: 600,\r\n");
                            sb.Append("                    height: 400,\r\n");
                            sb.Append("                    callBack: function (id) {\r\n");
                            sb.Append("                        return top[id].acceptClick(refreshGirdData);\r\n");
                            sb.Append("                    }\r\n");
                            sb.Append("                });\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "edit":
                            sb.Append("            // 编辑\r\n");
                            sb.Append("            $('#lr_edit').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerForm({\r\n");
                            sb.Append("                        id: 'form',\r\n");
                            sb.Append("                        title: '编辑',\r\n");
                            sb.Append("                        url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form?keyValue=' + keyValue,\r\n");
                            sb.Append("                        width: 600,\r\n");
                            sb.Append("                        height: 400,\r\n");
                            sb.Append("                        callBack: function (id) {\r\n");
                            sb.Append("                            return top[id].acceptClick(refreshGirdData);\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "delete":
                            sb.Append("            // 删除\r\n");
                            sb.Append("            $('#lr_delete').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerConfirm('是否确认删除该项！', function (res) {\r\n");
                            sb.Append("                        if (res) {\r\n");
                            sb.Append("                            learun.deleteForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/DeleteForm', { keyValue: keyValue}, function () {\r\n");
                            sb.Append("                                refreshGirdData();\r\n");
                            sb.Append("                            });\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "print":
                            sb.Append("            // 打印\r\n");
                            sb.Append("            $('#lr_print').on('click', function () {\r\n");
                            sb.Append("                $('#gridtable').jqprintTable();\r\n");
                            sb.Append("            });\r\n");
                            break;
                    }
                }

                foreach (var btn in colData.btnexs)
                {
                    sb.Append("            // " + btn.name + "\r\n");
                    sb.Append("            $('#" + btn.id + "').on('click', function () {\r\n");
                    sb.Append("            });\r\n");
                }

                sb.Append("        },\r\n");
                sb.Append("        // 初始化列表\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                // 判断是否分页
                if (colData.isPage == "1")
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetPageList',\r\n");
                }
                else
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetList',\r\n");
                }

                sb.Append("                headData: [\r\n");
                foreach (var col in colData.fields)
                {
                    sb.Append("                    { label: \"" + col.fieldName + "\", name: \"" + col.field + "\", width: " + col.width + ", align: \"" + col.align + "\"");

                    CodeFormCompontModel compont = compontMap[col.id];
                    if (compont.type == "select" || compont.type == "radio")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data['" + vlist[1] + "']);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "checkbox")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getsAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getsAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 textId: '" + vlist[1] + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "organize" || compont.type == "currentInfo")
                    {
                        if (compont.dataType == "company" || compont.dataType == "department" || compont.dataType == "user")
                        {
                            sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                            sb.Append("                             learun.clientdata.getAsync('" + compont.dataType + "', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.name);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    sb.Append("},\r\n");
                }
                sb.Append("                ],\r\n");
                sb.Append("                mainId:'" + mainPkey + "',\r\n");
                if (colData.isPage == "1")
                {
                    sb.Append("                isPage: true\r\n");
                }
                sb.Append("            });\r\n");
                if (queryData.isDate != "1")
                {
                    sb.Append("            page.search();\r\n");
                }
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("            param.StartTime = startTime;\r\n");
                    sb.Append("            param.EndTime = endTime;\r\n");
                }
                sb.Append("            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function () {\r\n");
                sb.Append("        $('#gridtable').jfGridSet('reload');\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 表单类
        /// <summary>
        /// 获取表单是否是必填字段标记
        /// </summary>
        /// <param name="verify"></param>
        /// <returns></returns>
        private string GetFontHtml(string verify)
        {
            var res = "";
            switch (verify)
            {
                case "NotNull":
                case "Num":
                case "Email":
                case "EnglishStr":
                case "Phone":
                case "Fax":
                case "Mobile":
                case "MobileOrPhone":
                case "Uri":
                    res = "<font face=\"宋体\">*</font>";
                    break;
            }
            return res;
        }
        /// <summary>
        /// 生成表单组件
        /// </summary>
        /// <param name="sb">字串容器</param>
        /// <param name="componts">组件列表</param>
        private void CreateFormCompont(StringBuilder sb, List<CodeFormCompontModel> componts)
        {
            foreach (var compont in componts)
            {
                if (compont.type == "label")
                {
                    sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\" style=\"padding:0;line-height:38px;text-align:center;font-size:20px;font-weight:bold;color:#333;\" >\r\n");
                    sb.Append("    <span>" + compont.title + "</span>\r\n");
                    sb.Append("    </div>\r\n");
                }
                else if (compont.type == "girdtable")
                {
                    sb.Append("    <div class=\"col-xs-12 lr-form-item lr-form-item-grid\" >\r\n");
                    sb.Append("        <div id=\"" + compont.table + "\"></div>\r\n");
                    sb.Append("    </div>\r\n");
                }
                else
                {
                    if (compont.isHide == "1")
                    {
                        sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\"  data-table=\"" + compont.table + "\" style=\"display: none; \" >\r\n");
                    }
                    else
                    {
                        sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\"  data-table=\"" + compont.table + "\" >\r\n");
                    }
                    //sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\"  data-table=\"" + compont.table + "\" >\r\n");
                    sb.Append("        <div class=\"lr-form-item-title\">" + compont.title + GetFontHtml(compont.verify) + "</div>\r\n");

                    string strValid = "";
                    if (!string.IsNullOrEmpty(compont.verify))
                    {
                        strValid = "isvalid=\"yes\" checkexpession=\"" + compont.verify + "\"";
                    }

                    switch (compont.type)
                    {
                        case "text":
                        case "datetimerange":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control\" " + strValid + " />\r\n");
                            break;
                        case "textarea":
                            sb.Append("        <textarea id=\"" + compont.field + "\" class=\"form-control\" style=\"height:" + compont.height + "px;\" " + strValid + " ></textarea>\r\n");
                            break;
                        case "texteditor":
                            sb.Append("        <div id=\"" + compont.field + "\" style=\"height:" + compont.height + "px;\"></div>\r\n");
                            break;
                        case "radio":
                        case "checkbox":
                            sb.Append("        <div id=\"" + compont.field + "\"></div>\r\n");
                            break;
                        case "select":
                        case "upload":
                        case "organize":
                            sb.Append("        <div id=\"" + compont.field + "\" " + strValid + " ></div>\r\n");
                            break;
                        case "datetime":
                            string dateformat = compont.dateformat == "0" ? "yyyy-MM-dd" : "yyyy-MM-dd HH:mm";
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control lr-input-wdatepicker\" onfocus=\"WdatePicker({ dateFmt:'" + dateformat + "',onpicked: function () { $('#" + compont.field + "').trigger('change'); } })\"  " + strValid + " />\r\n");
                            break;
                        case "encode":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" readonly class=\"form-control\" />\r\n");
                            break;
                        case "currentInfo":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" readonly class=\"form-control currentInfo lr-currentInfo-" + compont.dataType + "\" />\r\n");
                            break;
                    }
                    sb.Append("    </div>\r\n");
                }
            }
        }
        /// <summary>
        /// 表单类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础配置信息</param>
        /// <param name="formData">表单信息</param>
        /// <param name="compontMap">主键映射表</param>
        /// <returns></returns>
        public string FormCreate(BaseModel baseInfo, List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Form.cshtml\";\r\n");
                sb.Append("}\r\n");

                if (formData.Count == 1)// 一个选项卡的时候
                {
                    sb.Append("<div class=\"lr-form-wrap\">\r\n");
                    CreateFormCompont(sb, formData[0].componts);
                    sb.Append("</div>\r\n");
                }
                else// 多个选项卡的时候
                {
                    sb.Append("<div class=\"lr-form-tabs\" id=\"lr_form_tabs\">\r\n");
                    sb.Append("    <ul class=\"nav nav-tabs\">\r\n");
                    int num = 1;
                    foreach (var tab in formData)
                    {
                        sb.Append("        <li><a data-value=\"tab" + num + "\">" + tab.text + "</a></li>\r\n");
                        num++;
                    }
                    sb.Append("    </ul>\r\n</div>\r\n");
                    num = 1;
                    sb.Append("<div class=\"tab-content lr-tab-content\" id=\"lr_tab_content\">\r\n");
                    foreach (var tab in formData)
                    {
                        sb.Append("<div class=\"lr-form-wrap tab-pane\" id=\"tab" + num + "\">\r\n");
                        CreateFormCompont(sb, tab.componts);
                        sb.Append("</div>\r\n");

                        num++;
                    }
                    sb.Append("</div>\r\n");
                }
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Form.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 表单js类
        /// <summary>
        /// 构建编辑表头
        /// </summary>
        /// <param name="sb">构建容器</param>
        /// <param name="list">表头列表信息</param>
        /// <param name="table">表名</param>
        private void BulidGridHead(StringBuilder sb, List<TreeModelEx<CodeGridFieldModel>> list, string table)
        {
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.data.field))
                {
                    sb.Append("                    {\r\n");
                    sb.Append("                        label: '" + item.data.name + "', name: 'l" + Guid.NewGuid().ToString().Replace("-", "") + "', width:100, align: '" + item.data.align + "'");
                    if (item.ChildNodes != null && item.ChildNodes.Count > 0)
                    {
                        sb.Append("                        \r\n,children[");
                        BulidGridHead(sb, item.ChildNodes, table);
                        sb.Append("                        ]\r\n");
                    }
                    sb.Append("                    },\r\n");
                }
                else
                {
                    sb.Append("                    {\r\n");
                    sb.Append("                        label: '" + item.data.name + "', name: '" + item.data.field + "', width:" + item.data.width + ", align: '" + item.data.align + "'");
                    switch (item.data.type)
                    {
                        case "input":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'input'\r\n");
                            sb.Append("                        }\r\n");
                            break;
                        case "select":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'select',\r\n");
                            sb.Append("                            init: function (data, $edit) {\r\n");

                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + item.data.itemCode + "'\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            op:{\r\n");
                                sb.Append("                                value: '" + item.data.saveField + "',\r\n");
                                sb.Append("                                text:'" + item.data.showField + "',\r\n");
                                sb.Append("                                title:'" + item.data.showField + "'\r\n");
                                sb.Append("                            }\r\n");
                            }
                            sb.Append("                             }\r\n");
                            sb.Append("                        }\r\n");
                            break;
                        case "radio":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'radio',\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + item.data.itemCode + "',\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            text:'" + item.data.showField + "',\r\n");
                                sb.Append("                            value:'" + item.data.saveField + "',\r\n");
                            }
                            if (!string.IsNullOrEmpty(item.data.dfvalue))
                            {
                                sb.Append("                            dfvalue:'" + item.data.dfvalue + "'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "checkbox":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'checkbox',\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + item.data.itemCode + "',\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            text:'" + item.data.showField + "',\r\n");
                                sb.Append("                            value:'" + item.data.saveField + "',\r\n");
                            }
                            if (!string.IsNullOrEmpty(item.data.dfvalue))
                            {
                                sb.Append("                            dfvalue:'" + item.data.dfvalue + "'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "datetime":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'datatime',\r\n");
                            if (item.data.datetime == "date")
                            {
                                sb.Append("                            dateformat: '0'\r\n");
                            }
                            else
                            {
                                sb.Append("                            dateformat: '1'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "layer":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'layer',\r\n");
                            sb.Append("                            change: function (data, rownum, selectData) {\r\n");

                            foreach (var item2 in item.data.layerData)
                            {
                                if (string.IsNullOrEmpty(item2.value))
                                {
                                    continue;
                                }
                                sb.Append("                                data." + item2.value + " = selectData." + item2.name + ";\r\n");
                            }
                            sb.Append("                                $('#" + table + "').jfGridSet('updateRow', rownum);\r\n");
                            sb.Append("                            },\r\n");

                            sb.Append("                            op: {\r\n");
                            if (string.IsNullOrEmpty(item.data.layerW))
                            {
                                item.data.layerW = "600";
                            }
                            if (string.IsNullOrEmpty(item.data.layerH))
                            {
                                item.data.layerH = "400";
                            }

                            sb.Append("                                width: " + item.data.layerW + ",\r\n");
                            sb.Append("                                height: " + item.data.layerH + ",\r\n");
                            sb.Append("                                colData: [\r\n");

                            foreach (var item2 in item.data.layerData)
                            {
                                if (string.IsNullOrEmpty(item2.value))
                                {
                                    continue;
                                }
                                sb.Append("                                    { label: \"" + item2.label + "\", name: \"" + item2.name + "\", width: " + item2.width + ", align: \"" + item2.align + "\" },\r\n");
                            }
                            sb.Append("                                ],\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',\r\n");
                                sb.Append("                                param: { itemCode: '" + item.data.itemCode + "' }\r\n");
                            }
                            else
                            {
                                sb.Append("                                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataTable',\r\n");
                                sb.Append("                                param: { code: '" + item.data.dataSourceId + "'\r\n} ");
                            }

                            sb.Append("                            }\r\n");
                            sb.Append("                        }\r\n");
                            break;
                    }
                    sb.Append("                    },\r\n");
                }
            }
        }
        /// <summary>
        /// 表单JS类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础配置信息</param>
        /// <param name="dbTableList">数据表信息</param>
        /// <param name="formData">表单信息</param>
        /// <param name="compontMap">表单组件信息映射表</param>
        /// <returns></returns>
        public string FormJsCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2020 力软信息技术（苏州）有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var acceptClick;\r\n");
                sb.Append("var keyValue = request('keyValue');\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            $('.lr-form-wrap').lrscroll();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("            page.initData();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (formData.Count > 1)
                {
                    sb.Append("            $('#lr_form_tabs').lrFormTab();\r\n");
                    sb.Append("            $('#lr_form_tabs ul li').eq(0).trigger('click');\r\n");
                }

                // 编辑表格组件
                List<CodeFormCompontModel> girdcomponts = new List<CodeFormCompontModel>();


                foreach (var tab in formData)
                {
                    foreach (var compont in tab.componts)
                    {
                        switch (compont.type)
                        {
                            case "girdtable":
                                sb.Append("            $('#" + compont.table + "').jfGrid({\r\n");
                                sb.Append("                headData: [\r\n");

                                List<TreeModelEx<CodeGridFieldModel>> headTree = new List<TreeModelEx<CodeGridFieldModel>>();
                                foreach (var item in compont.fieldsData)
                                {
                                    TreeModelEx<CodeGridFieldModel> treeItem = new TreeModelEx<CodeGridFieldModel>();
                                    treeItem.id = item.id;
                                    treeItem.parentId = item.parentId;
                                    treeItem.data = item;

                                    headTree.Add(treeItem);
                                }
                                headTree.ToTree();

                                BulidGridHead(sb, headTree, compont.table);

                                sb.Append("                ],\r\n");
                                sb.Append("                isEdit: true,\r\n");
                                sb.Append("                height: 400\r\n");
                                sb.Append("            });\r\n");
                                girdcomponts.Add(compont);
                                break;
                            case "texteditor":
                                sb.Append("            var " + compont.field + "UE =  UE.getEditor('" + compont.field + "');\r\n");
                                sb.Append("            $('#" + compont.field + "')[0].ue =  " + compont.field + "UE;");
                                if (!string.IsNullOrEmpty(compont.dfvalue))
                                {
                                    sb.Append("            " + compont.field + "UE.ready(function () { \r\n");
                                    sb.Append("            " + compont.field + "UE.setContent(" + compont.dfvalue + ");\r\n");
                                    sb.Append("            });\r\n");
                                }
                                break;
                            case "radio":
                            case "checkbox":
                                sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                                sb.Append("                type: '" + compont.type + "',\r\n");
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("                code: '" + compont.itemCode + "',\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("                dataType: 'dataSource',\r\n");
                                    sb.Append("                code: '" + vlist[0] + "',\r\n");
                                    sb.Append("                value: '" + vlist[2] + "',\r\n");
                                    sb.Append("                text: '" + vlist[1] + "',\r\n");
                                }
                                sb.Append("            });\r\n");
                                break;
                            case "select":
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                                }
                                if (!string.IsNullOrEmpty(compont.dfvalue))
                                {
                                    sb.Append("            $('#" + compont.field + "').lrselectSet(\"" + compont.dfvalue + "\");\r\n");
                                }
                                break;
                            case "datetimerange":
                                if (!string.IsNullOrEmpty(compont.startTime) && !string.IsNullOrEmpty(compont.endTime))
                                {
                                    sb.Append("            $('#" + compontMap[compont.startTime].field + "').on('change', function () {\r\n");
                                    sb.Append("                var st = $(this).val();\r\n");
                                    sb.Append("                var et = $('#" + compontMap[compont.endTime].field + "').val();\r\n");
                                    sb.Append("                if (!!st && !!et) {\r\n");
                                    sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                    sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                    sb.Append("                }\r\n");
                                    sb.Append("            });\r\n");

                                    sb.Append("            $('#" + compontMap[compont.endTime].field + "').on('change', function () {\r\n");
                                    sb.Append("                var et = $('#" + compontMap[compont.startTime].field + "').val();\r\n");
                                    sb.Append("                var et = $(this).val();\r\n");
                                    sb.Append("                if (!!st && !!et) {\r\n");
                                    sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                    sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                    sb.Append("                }\r\n");
                                    sb.Append("            });\r\n");
                                }
                                break;
                            case "encode":
                                sb.Append("            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/CodeRule/GetEnCode', { code: '" + compont.rulecode + "' }, function (data) {\r\n");
                                sb.Append("                if (!$('#" + compont.field + "').val()) {\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(data);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");
                                break;
                            case "organize":
                                switch (compont.dataType)
                                {
                                    case "user"://用户
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrformselect({\r\n");
                                            sb.Append("                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',\r\n");
                                            sb.Append("                layerUrlW: 400,\r\n");
                                            sb.Append("                layerUrlH: 300,\r\n");
                                            sb.Append("                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                value: 'F_UserId',\r\n");
                                            sb.Append("                text: 'F_RealName',\r\n");
                                            sb.Append("                title: 'F_RealName',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");
                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                if (value == '')\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: '',\r\n");
                                            sb.Append("                        data: []\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("                else\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList',\r\n");
                                            sb.Append("                        param: { departmentId: value }\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("            })\r\n");
                                        }
                                        break;
                                    case "department"://部门
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true,\r\n");
                                            sb.Append("                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                param: {} \r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");

                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                    param: { companyId: value }\r\n");
                                            sb.Append("                });\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        break;
                                    case "company"://公司
                                        sb.Append("            $('#" + compont.field + "').lrCompanySelect({});\r\n");
                                        break;
                                }
                                break;
                            case "currentInfo":
                                switch (compont.dataType)
                                {
                                    case "company":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).companyId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('company', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).companyId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "department":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).departmentId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('department', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).departmentId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "user":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).userId;\r\n");
                                        sb.Append("            $('#" + compont.field + "').val(learun.clientdata.get(['userinfo']).realName);\r\n");
                                        break;
                                    case "time":
                                        sb.Append("            $('#" + compont.field + "').val(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));\r\n");
                                        break;
                                }
                                break;
                            case "upload":
                                sb.Append("            $('#" + compont.field + "').lrUploader();\r\n");
                                break;
                        }
                    }
                }
                sb.Append("        },\r\n");
                sb.Append("        initData: function () {\r\n");
                sb.Append("            if (!!keyValue) {\r\n");
                sb.Append("                $.lrSetForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetFormData?keyValue=' + keyValue, function (data) {\r\n");
                sb.Append("                    for (var id in data) {\r\n");
                sb.Append("                        if (!!data[id].length && data[id].length > 0) {\r\n");

                sb.Append("                            $('#' + id ).jfGridSet('refreshdata', data[id]);\r\n");

                sb.Append("                        }\r\n");
                sb.Append("                        else {\r\n");
                sb.Append("                            $('[data-table=\"' + id + '\"]').lrSetFormData(data[id]);\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    // 保存数据\r\n");
                sb.Append("    acceptClick = function (callBack) {\r\n");
                sb.Append("        if (!$('body').lrValidform()) {\r\n");
                sb.Append("            return false;\r\n");
                sb.Append("        }\r\n");

                if (dbTableList.Count == 1)
                {
                    sb.Append("        var postData = {\r\n");
                    sb.Append("            strEntity: JSON.stringify($('body').lrGetFormData())\r\n");
                    sb.Append("        };\r\n");
                }
                else
                {
                    sb.Append("        var postData = {};\r\n");
                    foreach (var table in dbTableList)
                    {
                        if (girdcomponts.FindAll(t => t.table == table.name).Count >= 1)
                        {
                            sb.Append("        postData.str" + Str.FirstLower(table.name) + "List = JSON.stringify($('#" + table.name + "').jfGridGet('rowdatas'));\r\n");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(table.relationName))
                            {
                                sb.Append("        postData.strEntity = JSON.stringify($('[data-table=\"" + table.name + "\"]').lrGetFormData());\r\n");
                            }
                            else
                            {
                                sb.Append("        postData.str" + Str.FirstLower(table.name) + "Entity = JSON.stringify($('[data-table=\"" + table.name + "\"]').lrGetFormData());\r\n");
                            }
                        }
                    }
                }
                sb.Append("        $.lrSaveForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/SaveForm?keyValue=' + keyValue, postData, function (res) {\r\n");
                sb.Append("            // 保存成功后才回调\r\n");
                sb.Append("            if (!!callBack) {\r\n");
                sb.Append("                callBack();\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region webapi接口类
        /// <summary>
        /// webapi接口类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string ApiCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Nancy;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");

                sb.Append("namespace Learun.Application.WebApi\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Api : BaseApi\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");
                #endregion

                #region 注册接口地址
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 注册接口\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        public " + baseInfo.name + "Api()\r\n");
                sb.Append("            : base(\"/learun/adms/" + baseInfo.outputArea + "/" + baseInfo.name + "\")\r\n");
                sb.Append("        {\r\n");
                sb.Append("            Get[\"/pagelist\"] = GetPageList;\r\n");
                sb.Append("            Get[\"/list\"] = GetList;\r\n");
                sb.Append("            Get[\"/form\"] = GetForm;\r\n");

                sb.Append("            Post[\"/delete\"] = DeleteForm;\r\n");
                sb.Append("            Post[\"/save\"] = SaveForm;\r\n");

                sb.Append("        }\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表分页数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"_\"></param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public Response GetPageList(dynamic _)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            ReqPageParam parameter = this.GetReqData<ReqPageParam>();\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetPageList(parameter.pagination, parameter.queryJson);\r\n");
                sb.Append("            var jsonData = new\r\n");
                sb.Append("            {\r\n");
                sb.Append("                rows = data,\r\n");
                sb.Append("                total = parameter.pagination.total,\r\n");
                sb.Append("                page = parameter.pagination.page,\r\n");
                sb.Append("                records = parameter.pagination.records\r\n");
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"_\"></param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public Response GetList(dynamic _)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            string queryJson = this.GetReqData();\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                sb.Append("            return Success(data);\r\n");
                sb.Append("        }\r\n");

                // 获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"_\"></param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public Response GetForm(dynamic _)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            string keyValue = this.GetReqData();\r\n");
                string strEntityCode = GetEntityCode(TableTree, girdDbTableList, baseInfo, "");
                sb.Append(strEntityCode);
                sb.Append("            var jsonData = new {\r\n");
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("                " + tableOne.name + " = " + tableOne.name + "Data,\r\n");
                }
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"_\"></param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public Response DeleteForm(dynamic _)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            string keyValue = this.GetReqData();\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"_\"></param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");


                // 函数传入参数
                string paramStr = "";
                string paramStr2 = "this.userInfo,parameter.keyValue,entity,";
                string paramStr3 = "            " + mainTable + "Entity entity = parameter.strEntity.ToObject<" + mainTable + "Entity>();\r\n";

                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += "            public string str" + Str.FirstLower(tableOne.name) + "Entity{get;set;}\r\n";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr3 += "            " + tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity = parameter.str" + Str.FirstLower(tableOne.name) + "Entity.ToObject<" + tableOne.name + "Entity>();\r\n";
                        }
                        else
                        {
                            paramStr += "            public string str" + Str.FirstLower(tableOne.name) + "List{get;set;}\r\n";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                            paramStr3 += "            List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List = parameter.str" + Str.FirstLower(tableOne.name) + "List.ToObject<List<" + tableOne.name + "Entity>>();\r\n";

                        }
                    }
                }
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public Response SaveForm(dynamic _)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            ReqFormEntity parameter = this.GetReqData<ReqFormEntity>();\r\n");
                sb.Append(paramStr3);
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveEntity(" + paramStr2 + ");\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                #region 定义一个实体类用来接收表单数据
                sb.Append("        #region 私有类\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单实体类\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        private class ReqFormEntity {\r\n");
                sb.Append("            public string keyValue { get; set; }\r\n");
                sb.Append("            public string strEntity{ get; set; }\r\n");
                sb.Append(paramStr);
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion


                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 移动页面主页面
        /// <summary>
        /// 移动页面主页面创建(移动开发模板) 
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTableList"></param>
        /// <param name="compontMap"></param>
        /// <param name="colData"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string AppIndexCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData, QueryModel queryData)
        {
            try
            {
                var sb = new StringBuilder();
                void add(string str) { sb.Append(str + "\r\n"); }

                var mainTable = dbTableList.Find(t => string.IsNullOrEmpty(t.relationName));
                var mainPk = mainTable?.pk ?? "F_Id";
                var apiPath = $"/{ baseInfo.outputArea }/{ baseInfo.name }";

                // Vue模板
                add("<template>");
                add("  <view class=\"page\">");
                add("    <scroll-view @scrolltolower=\"fetchList\" :class=\"sideOpen ? 'show' : ''\" style=\"padding-top: 100rpx\" class=\"mainpage solid-top\" scroll-y>");
                // 列表头
                add("      <view class=\"custom-list-header\">");
                add("        <view class=\"custom-list-banner\">");
                add("          {{ pageInfo }}");
                add("          <view class=\"custom-list-action\">");
                add("            <l-icon @click=\"sideOpen = true\" class=\"text-xxl\" type=\"searchlist\" color=\"blue\" />");
                add("          </view>");
                add("        </view>");
                add("      </view>");
                add("");

                // 主列表
                add("      <view class=\"list\">");
                add($"        <view class=\"custom-item\" v-for=\"(item, index) of list\" :key=\"item.{ mainPk }\">");
                // 渲染列表各项标题
                // 因为涉及到数据源到显示文本之间的转换，所以由前端调用 displayListItem 来展示
                colData.fields.ForEach(field => {
                    add("          <view class=\"custom-item-main\">");
                    add("            <text class=\"custom-item-title\">" + field.fieldName + "：</text>");
                    add("            {{ displayListItem(item, '" + field.field + "') }}");
                    add("          </view>");
                });
                add("");
                // 渲染列表操作区按钮
                add("          <view class=\"custom-action\">");
                // 删除按钮，需要则渲染
                if (Array.Exists(colData.btns, t => t == "delete"))
                {
                    add($"            <view @click=\"deleteItem(item.{ mainPk }, index)\" class=\"custom-action-btn line-red text-sm\" style=\"border: currentColor 1px solid\">");
                    add($"              <l-icon type=\"delete\" />");
                    add($"              删除");
                    add($"            </view>");
                }
                // 编辑按钮，需要则渲染
                if (Array.Exists(colData.btns, t => t == "edit"))
                {
                    add($"            <view @click=\"action('edit', item.{ mainPk })\" class=\"custom-action-btn line-blue text-sm\" style=\"border: currentColor 1px solid\">");
                    add($"              <l-icon type=\"edit\" />");
                    add($"              编辑");
                    add($"            </view>");
                }
                // 查看按钮，必须渲染
                add($"            <view @click=\"action('view', item.{ mainPk })\" class=\"custom-action-btn line-blue text-sm\" style=\"border: currentColor 1px solid;min-width: 160rpx;\">");
                add($"              查看");
                add($"              <l-icon type=\"right\" />");
                add($"            </view>");
                add("          </view>");
                add("        </view>");
                add("      </view>");
                add("");
                add("      <view class=\"custom-item\">");
                add("        {{ page >= total ? `已加载全部条目` : `加载中...` }}");
                add("      </view>");
                add("    </scroll-view>");
                add("");

                // 侧边栏的返回按钮
                add("    <view class=\"sideclose\" :class=\"sideOpen ? 'show' : ''\" @click=\"sideOpen = false\">");
                add("      <l-icon type=\"pullright\" color=\"blue\" />");
                add("    </view>");
                add("");

                // 侧边栏，用于渲染筛选相关的表单
                add("    <scroll-view scroll-y class=\"sidepage\" :class=\"sideOpen ? 'show' : ''\">");
                add("      <view v-if=\"ready\" class=\"padding\">");
                // 按时间日期筛选
                if (int.Parse(queryData.isDate) == 1)
                {
                    add("        <view class=\"side-title\">按时间筛选：</view>");
                    add("        <l-button @click=\"changeDateRange('all')\" :line=\"dateRange !== 'all' ? 'green' : ''\" :color=\"dateRange === 'all' ? 'green' : ''\" class=\"block margin-top-sm\" block>全部</l-button>");
                    add("        <l-button @click=\"changeDateRange('today')\" :line=\"dateRange !== 'today' ? 'blue' : ''\" :color=\"dateRange === 'today' ? 'blue' : ''\" class=\"block margin-top-sm\" block>今天</l-button>");
                    add("        <l-button @click=\"changeDateRange('7d')\" :line=\"dateRange !== '7d' ? 'blue' : ''\" :color=\"dateRange === '7d' ? 'blue' : ''\" class=\"block margin-top-sm\" block>最近7天内</l-button>");
                    add("        <l-button @click=\"changeDateRange('1m')\" :line=\"dateRange !== '1m' ? 'blue' : ''\" :color=\"dateRange === '1m' ? 'blue' : ''\" class=\"block margin-top-sm\" block>最近1个月内</l-button>");
                    add("        <l-button @click=\"changeDateRange('3m')\" :line=\"dateRange !== '3m' ? 'blue' : ''\" :color=\"dateRange === '3m' ? 'blue' : ''\" class=\"block margin-top-sm\" block>最近3个月内</l-button>");
                    add("        <view v-if=\"dateRange === 'custom'\" class=\"side-title\">自定义时间区间：</view>");
                    add("        <l-date-picker v-if=\"dateRange === 'custom'\" v-model=\"startDate\" @change=\"searchChange\" title=\"起始时间\" placeholder=\"点击来选取\" />");
                    add("        <l-date-picker v-if=\"dateRange === 'custom'\" v-model=\"endDate\" @change=\"searchChange\" title=\"结束时间\" placeholder=\"点击来选取\" />");
                    add("");
                }

                // 渲染筛选项
                var queryFields = (from kv in compontMap from query in queryData.fields where kv.Key == query.id select kv.Value).ToList();
                queryFields.ForEach(item => {
                    switch (item.type)
                    {
                        case "text":
                        case "textarea":
                        case "texteditor":
                        case "datetimerange":
                        case "encode":
                            add($"        <l-input");
                            add($"          v-model=\"queryData.{ item.field }\"");
                            add($"          @change=\"searchChange\"");
                            add($"          title =\"{ item.title }\"");
                            add($"          placeholder=\"按{ item.title }筛选\"");
                            add($"          arrow");
                            add($"        />");
                            break;

                        case "datetime":
                            if (int.Parse(item.dateformat) == 1)
                            {
                                add($"        <l-datetime-picker");
                                add($"          v-model=\"queryData.{ item.field }\"");
                                add($"          @change=\"searchChange\"");
                                add($"          title =\"{ item.title }\"");
                                add($"          placeholder=\"按{ item.title }筛选\"");
                                add($"          arrow");
                                add($"        />");
                            }
                            else
                            {
                                add($"        <l-date-picker");
                                add($"          v-model=\"queryData.{ item.field }\"");
                                add($"          @change=\"searchChange\"");
                                add($"          title =\"{ item.title }\"");
                                add($"          placeholder=\"按{ item.title }筛选\"");
                                add($"          arrow");
                                add($"        />");
                            }

                            break;

                        case "checkbox":
                            add($"        <l-checkbox-picker");
                            add($"          v-model=\"queryData.{ item.field }\"");
                            add($"          @change=\"searchChange\"");
                            add($"          :range=\"dataSource.{ item.field }\"");
                            add($"          title =\"{ item.title }\"");
                            add($"          placeholder=\"按{ item.title }筛选\"");
                            add($"          arrow");
                            add($"        />");
                            break;

                        case "radio":
                        case "select":
                            add($"        <l-select");
                            add($"          v-model=\"queryData.{ item.field }\"");
                            add($"          @change=\"searchChange\"");
                            add($"          :range=\"dataSource.{ item.field }\"");
                            add($"          title =\"{ item.title }\"");
                            add($"          placeholder=\"按{ item.title }筛选\"");
                            add($"          arrow");
                            add($"        />");
                            break;

                        case "currentInfo":
                        case "organize":
                            if (item.dataType != "time")
                            {
                                add($"        <l-organize-picker");
                                add($"          v-model=\"queryData.{ item.field }\"");
                                add($"          @change=\"searchChange\"");
                                add($"          type=\"{ item.dataType }\"");
                                add($"          title =\"{ item.title }\"");
                                add($"          placeholder=\"按{ item.title }筛选\"");
                                add($"          arrow");
                                add($"        />");
                            }
                            else
                            {
                                add($"        <l-date-picker");
                                add($"          v-model=\"dataSource.{ item.field }\"");
                                add($"          @change=\"searchChange\"");
                                add($"          type=\"{ item.dataType }\"");
                                add($"          title =\"{ item.title }\"");
                                add($"          placeholder=\"按{ item.title }筛选\"");
                                add($"          arrow");
                                add($"        />");
                            }
                            break;

                        default: break;
                    }
                });
                add("");

                add("        <view class=\"padding-tb\">");
                add("          <l-button class=\"block\" block line=\"orange\" @click=\"reset\">重置筛选条件</l-button>");
                add("        </view>");
                add("      </view>");
                add("    </scroll-view>");

                // 新建按钮，需要则渲染
                if (Array.Exists(colData.btns, t => t == "add"))
                {
                    add("    <l-custom-add @click=\"action('create')\" v-if=\"!sideOpen\" />");
                }

                add("  </view>");
                add("</template>");

                add("");
                add("");

                // JS代码
                add("<script>");
                add("/*");
                add(" * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)");
                add($" * Copyright (c) 2013-{ DateTime.Now.Year } 上海力软信息技术有限公司");
                add(" * 创建人：" + LoginUserInfo.Get()?.realName);
                add(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                add(" * 描  述：" + baseInfo.describe);
                add(" */");
                add("");

                add("import moment from 'moment'");
                add("import _ from 'lodash'");
                add("");

                add("export default {");
                add($"  name: 'l-{ baseInfo.outputArea + baseInfo.name }',");
                add("");

                // data
                add("  data() {");
                add("    return {");
                add("      // 数据项的数据类型、结构");
                add("      scheme: {");
                // 依次生成数据项对象
                var fields = (from kv in compontMap from colItem in colData.fields where kv.Key == colItem.id select kv.Value).ToList();
                fields.ForEach(field => {
                    var schemeObjStr = "        " + field.field + ": { type: '" + field.type + "'";
                    if (!string.IsNullOrWhiteSpace(field.itemCode)) { schemeObjStr += $", itemCode: '{field.itemCode}'"; }
                    if (!string.IsNullOrWhiteSpace(field.dataType)) { schemeObjStr += $", dataType: '{field.dataType}'"; }
                    if (!string.IsNullOrWhiteSpace(field.dataSource)) { schemeObjStr += $", dataSource: '{field.dataSource}'"; }
                    if (!string.IsNullOrWhiteSpace(field.dataSourceId)) { schemeObjStr += $", dataSourceId: '{field.dataSourceId}'"; }
                    if (!string.IsNullOrWhiteSpace(field.dateformat)) { schemeObjStr += $", dateformat: '{field.dateformat}'"; }

                    add(schemeObjStr + " },");
                });
                add("      },");
                add("");

                add("      // 筛选菜单值");
                add("      defaultQueryData: {},");
                add("      queryData: {");
                // 依次生成筛选菜单model
                queryFields.ForEach(item => {
                    var value = item.type == "checkbox" ? "[]" : "''";
                    add($"        { item.field }: { value },");
                });
                add("      },");
                add("");

                add("      // 数据源");
                add("      dataSource: {");
                // 依次生成数据源数组
                string[] needSourceType = { "checkbox", "radio", "select" };
                var dataSource = (from kv in compontMap where needSourceType.Contains(kv.Value.type) select kv.Value).ToList();
                dataSource.ForEach(item => {
                    if (int.Parse(item.dataSource) == 0)
                    {
                        add($"        { item.field }: " + "Object.values(this.$store.state.propTable." + item.itemCode + ").map(t => ({ value: t.value, text: t.text })),");
                    }
                    else
                    {
                        add($"        { item.field }: [],");
                    }
                });
                add("      },");
                add("");

                if (int.Parse(queryData.isDate) == 1)
                {
                    add("      dateRange: 'all',");
                    add("      startDate: null,");
                    add("      endDate: null,");
                }
                add("");

                add("      page: 1,");
                add("      total: 2,");
                add("      list: [],");
                add("      searchData: {},");
                add("      pageInfo: '(请等待页面加载完成...)',");
                add("      sideOpen: false,");
                add("      ready: false");
                add("    }");
                add("  },");
                add("");

                // 页面生命周期
                add("  async onLoad() {");
                add("    await this.init()");
                add("  },");
                add("  onUnload() {");
                add($"    uni.$off('{ baseInfo.outputArea + baseInfo.name }-list-change')");
                add("  },");
                add("");

                var dsReqStr = new StringBuilder();
                var syncDsReqStr = new StringBuilder();
                void dsReqAdd(string str) => dsReqStr.Append(str).Append("\r\n");

                dataSource.ForEach(item => {
                    if (int.Parse(item.dataSource) != 0)
                    {
                        var dataSourceInfo = item.dataSourceId?.Split(',');
                        dsReqAdd("        uni.request({");
                        dsReqAdd("          url: this.apiRoot + '/datasource/map',");
                        dsReqAdd("          data: { ...this.auth, data: JSON.stringify({code: '" + dataSourceInfo[0] + "', ver: '' }) }");
                        dsReqAdd("        }).then(([err, { data: { data: result } } = {}]) => {");
                        dsReqAdd("          this.dataSource." + item.field + " = result.data.map(t => ({ text: t." + dataSourceInfo[1] + ", value: t." + dataSourceInfo[2] + " }))");
                        dsReqAdd("        }),");
                    }
                });

                // methods
                add("  methods: {");
                add("    // 初始化");
                add("    async init() {");
                add($"      uni.$on('{ baseInfo.outputArea + baseInfo.name }-list-change', this.refresh)");
                add("      // 加载列表和数据源");
                add("");
                add(syncDsReqStr.ToString());
                add("");
                add("      await Promise.all([");
                add(dsReqStr.ToString());
                add("        this.fetchList()");
                add("      ])");
                add("      this.defaultQueryData = JSON.parse(JSON.stringify(this.queryData))");
                add("      this.ready = true");
                add("    },");
                add("");

                add("    // 请求api，以获取列表数据");
                add("    async fetchList(e) {");
                add("      if (e && e.preventDefault) { e.preventDefault() }");
                add("      if (this.page > this.total) { return }");
                add("");
                add("      uni.showLoading({ title: '加载订单信息...', mask: true })");
                add("      const [err, { data: { data: result } } = {}] = await uni.request({");
                add("        url: this.apiRoot + '" + apiPath + "/pagelist',");
                add("        data: {");
                add("          ...this.auth,");
                add("          data: JSON.stringify({");
                add("            pagination: { rows: 10, page: this.page, sidx: '" + mainPk + "', sord: 'DESC' },");
                add("            queryJson: JSON.stringify(this.searchData)");
                add("          })");
                add("        }");
                add("      })");
                add("");
                add("      if (err || !result) {");
                add("        uni.hideLoading()");
                add("        uni.showToast({ title: '加载数据时出错', icon: 'none' })");
                add("      }");
                add("      this.total = result.total");
                add("      this.page = this.page + 1");
                add("      this.list = this.list.concat(result.rows)");
                add("      this.pageInfo = `已加载 ${result.page} / ${result.total} 页，合计共 ${result.records} 条记录`");
                add("      uni.hideLoading()");
                add("    },");
                add("");

                add("    // 显示列表中的标题项");
                add("    displayListItem(item, field) {");
                add("      const fieldItem = this.scheme[field]");
                add("      const value = item[field]");
                add("");
                add("      switch (fieldItem.type) {");
                add("        case 'currentInfo':");
                add("        case 'organize':");
                add("          switch (fieldItem.dataType) {");
                add("            case 'user': return _.get(this.$store.state.staff, `${value}.name`, '')");
                add("            case 'department': return _.get(this.$store.state.dep, `${value}.name`, '')");
                add("            case 'company': return _.get(this.$store.state.company, `${value}.name`, '')");
                add("            default: return value || ''");
                add("          }");
                add("");
                add("        case 'radio':");
                add("        case 'select':");
                add("          const selectItem = this.sourceData[field].find(t => t.value === value)");
                add("          return _.get(selectItem, 'text', '')");
                add("");
                add("        case 'checkbox':");
                add("          if (!value || value.split(',').length <= 0) { return '' }");
                add("          const checkboxItems = value.split(',')");
                add("          return this.sourceData[field].filter(t => checkboxItems.includes(t.value)).map(t => t.text).join('，')");
                add("");
                add("        case 'datetime':");
                add("          if (!value) { return '' }");
                add("          return moment(value).format(Number(fieldItem.dateformat) === 0 ? 'YYYY-MM-DD' : 'YYYY-MM-DD HH:mm:ss')");
                add("");
                add("        default: return value || ''");
                add("      }");
                add("    },");
                add("");

                add("    // 列表内容发生变化，刷新列表");
                add("    async refresh(e) {");
                add("      this.page = 1");
                add("      this.total = 2");
                add("      this.list = []");
                add("      await this.fetchList()");
                add("    },");
                add("");

                add("    // 设置搜索条件");
                add("    async searchChange() {");
                add("      const result = {}");
                add("");
                // 开启时间日期查询，则添加相关代码
                if (int.Parse(queryData.isDate) == 1)
                {
                    add("      const todayEnd = moment().format('YYYY-MM-DD 23:59:59')");
                    add("      if (dateRange === 'today') {");
                    add("        result.StartTime = moment().subtract(1, 'day').format('YYYY-MM-DD 00:00:00')");
                    add("        result.EndTime = todayEnd");
                    add("      } else if (dateRange === '7d') {");
                    add("        result.StartTime = moment().subtract(7, 'days').format('YYYY-MM-DD 00:00:00')");
                    add("        result.EndTime = todayEnd");
                    add("      } else if (dateRange === '1m') {");
                    add("        result.StartTime = moment().subtract(1, 'month').format('YYYY-MM-DD 00:00:00')");
                    add("        result.EndTime = todayEnd");
                    add("      } else if (dateRange === '3m') {");
                    add("        result.StartTime = moment().subtract(3, 'months').format('YYYY-MM-DD 00:00:00')");
                    add("        result.EndTime = todayEnd");
                    add("      } else if (dateRange === 'custom' && (startDate || startDate)) {");
                    add("        if (!(startDate && startDate && moment(startDate).isAfter(endDate))) {");
                    add("          result.StartTime = startDate ? moment(startDate).format('YYYY-MM-DD 00:00:00') : '1900-01-01 00:00:00'");
                    add("          result.EndTime = endDate ? moment(endDate).format('YYYY-MM-DD 23:59:59') : todayEnd");
                    add("        }");
                    add("      }");
                    add("");
                }

                add("      const queryObj = _.pickBy(this.queryData, t => (Array.isArray(t) ? t.length > 0 : t))");
                add("      Object.assign(result, _.mapValues(queryObj, t => (Array.isArray(t) ? t.join(',') : t)))");
                add("");

                add("      this.searchData = result");
                add("      await this.refresh()");
                add("    },");
                add("");

                add("    // 点击「清空搜索条件」按钮");
                add("    reset() {");
                // 开启时间日期查询，则添加相关代码
                if (int.Parse(queryData.isDate) == 1)
                {
                    add("      this.dateRange = 'all'");
                    add("      this.startDate = null");
                    add("      this.endDate = null");
                    add("");
                }
                add("      this.queryData = JSON.parse(JSON.stringify(this.defaultQueryData))");
                add("");

                add("      this.searchChange()");
                add("    },");
                add("");

                add("    // 点击「编辑」、「查看」、「添加」按钮");
                add("    action(type, id = 'no') {");
                add("      uni.navigateTo({ url: `./single?type=${type}&id=${id}` })");
                add("    },");
                add("");

                // 需要删除按钮时生成此方法
                if (Array.Exists(colData.btns, t => t == "delete"))
                {
                    add("    // 点击「删除」按钮");
                    add("    async deleteItem(id, index) {");
                    add("      uni.showModal({");
                    add("        title: '删除项目', ");
                    add("        content: '确定要删除该项吗？',");
                    add("        success: ({ confirm }) => {");
                    add("          if (!confirm) { return }");
                    add("          uni");
                    add("            .request({");
                    add("              url: this.apiRoot + '" + apiPath + "/delete',");
                    add("              method: 'POST',");
                    add("              header: { 'content-type': 'application/x-www-form-urlencoded' },");
                    add("              data: { ...this.auth, data: id }");
                    add("            })");
                    add("            .then(([err, { data }]) => {");
                    add("              if (err || !data || data.code !== 200) {");
                    add("                uni.showToast({ title: '删除失败', icon: 'none' })");
                    add("                return");
                    add("              }");
                    add("              uni.showToast({ title: '删除成功', icon: 'success' })");
                    add("              this.refresh()");
                    add("            })");
                    add("        }");
                    add("      })");
                    add("    },");
                    add("");
                }
                add("  }");
                add("");
                add("}");
                add("</script>");
                add("");
                add("");

                // LESS 样式
                add("<style lang=\"less\" scoped>");
                add("@import '~@/common/css/sidepage.less';");
                add("@import '~@/common/css/custom-item.less';");
                add("</style>");

                return sb.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 移动表单类

        /// <summary>
        /// 移动表单类创建(移动开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTableList"></param>
        /// <param name="formData"></param>
        /// <param name="compontMap"></param>
        /// <returns></returns>
        public string AppFormCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                var sb = new StringBuilder();
                void add(string t) => sb.Append(t).Append("\r\n");

                var mainTable = dbTableList.Find(t => string.IsNullOrEmpty(t.relationName));
                var mainPk = mainTable?.pk ?? "F_Id";
                var apiPath = $"/{ baseInfo.outputArea }/{ baseInfo.name }";

                var fields = (from item in compontMap.Values where item.type == "girdtable" || !string.IsNullOrWhiteSpace(item.field) select item).ToList();

                // Vue模板
                add("<template>");
                add("  <view class=\"page\">");

                add("    <view v-if=\"ready\">");
                fields.ForEach(item => {
                    switch (item.type)
                    {
                        case "label":
                            add("      <l-title border>" + item.title + "</l-title>");
                            break;

                        case "text":
                            add($"      <l-input");
                            add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                            add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                            add($"        :disabled=\"!edit\"");
                            add($"        title=\"{ item.title }\"");
                            add($"        placeholder=\"请输入{ item.title }\"");
                            add($"        right");
                            if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                            add($"      />");
                            break;

                        case "radio":
                        case "select":
                            add($"      <l-select");
                            add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                            add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                            add($"        :disabled=\"!edit\"");
                            add($"        :arrow=\"edit\"");
                            add($"        :range=\"dataSource.{ item.table }.{ item.field }\"");
                            add($"        title=\"{ item.title }\"");
                            add($"        placeholder=\"请选择{ item.title }\"");
                            if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                            add($"      />");
                            break;

                        case "checkbox":
                            add($"      <l-checkbox-picker");
                            add($"        @open=\"modal = 'checkbox'\"");
                            add($"        @close=\"modal = null\"");
                            add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                            add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                            add($"        :readonly=\"!edit\"");
                            add($"        :arrow=\"edit\"");
                            add($"        :range=\"dataSource.{ item.table }.{ item.field }\"");
                            add($"        title=\"{ item.title }\"");
                            add($"        placeholder=\"请选择{ item.title }\"");
                            if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                            add($"      />");
                            break;

                        case "textarea":
                        case "texteditor":
                            add($"      <l-textarea");
                            add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                            add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                            add($"        :hide=\"modal\"");
                            add($"        :readonly=\"!edit\"");
                            add($"        title=\"{ item.title }\"");
                            add($"        placeholder=\"请输入{ item.title }\"");
                            add($"        formMode");
                            if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                            add($"      />");
                            break;

                        case "datetime":
                            if (int.Parse(item.dateformat) == 1)
                            {
                                add($"      <l-datetime-picker");
                                add($"        @open=\"modal = 'datetime'\"");
                                add($"        @close=\"modal = null\"");
                                add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                                add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                                add($"        :disabled=\"!edit\"");
                                add($"        :arrow=\"edit\"");
                                add($"        title=\"{ item.title }\"");
                                add($"        placeholder=\"请选择{ item.title }\"");
                                if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                                add($"      />");
                            }
                            else
                            {
                                add($"      <l-date-picker");
                                add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                                add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                                add($"        :disabled=\"!edit\"");
                                add($"        :arrow=\"edit\"");
                                add($"        title=\"{ item.title }\"");
                                add($"        placeholder=\"请选择{ item.title }\"");
                                if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                                add($"      />");
                            }
                            break;

                        case "currentInfo":
                        case "encode":
                        case "datetimerange":
                            add($"      <l-label title=\"{ item.title }\"{ (string.IsNullOrWhiteSpace(item.verify) ? "" : " required") } >");
                            add(@"        {{ displayItem('" + item.table + "." + item.field + "') }}");
                            add($"      </l-label>");
                            break;

                        case "organize":
                            add($"      <l-organize-picker");
                            add($"        @input=\"setValue('{ item.table }.{ item.field }', $event)\"");
                            add($"        :value=\"getValue('{ item.table }.{ item.field }')\"");
                            add($"        :readonly=\"!edit\"");
                            add($"        :arrow=\"edit\"");
                            add($"        type=\"{ item.dataType }\"");
                            add($"        title=\"{ item.title }\"");
                            add($"        placeholder=\"请选择{ item.title }\"");
                            if (!string.IsNullOrWhiteSpace(item.verify)) { add("        required"); }
                            add($"      />");
                            break;

                        case "upload":
                            add($"      <template>");
                            add($"        <view class=\"cu-form-group\" style=\"border-bottom: none\">");
                            add($"          <view class=\"title\">");
                            add($"            <text v-if=\"item.verify\" style=\"color:red;font-size: 1.2em;\">*</text>");
                            add(@"            {{ item.title }}");
                            add($"          </view>");
                            add($"        </view>");
                            add($"        <l-upload");
                            add($"          v-model=\"current.{ item.table }.{ item.field }\"");
                            add($"          :readonly=\"!edit\"");
                            add($"          :number=\"1\"");
                            add($"        />");
                            add($"      </template>");
                            break;

                        case "html":
                            add($"      <view class=\"cu-form-group\">");
                            add($"        <view class=\"bg-white\">");
                            add($"          { item.title }");
                            add($"        </view>");
                            add($"      </view>");
                            break;

                        case "girdtable":
                            add("");
                            add($"      <view v-for=\"(tableItem, tableIndex) of current.{ item.table }\" :key=\"tableIndex\">");
                            add("        <view class=\"table-item padding-lr\">");
                            add("          <view class=\"table-item-title\">" + item.title + " (第{{ tableIndex + 1 }}项)</view>");
                            add($"          <view v-if=\"tableIndex !== 0 && edit\" class=\"table-item-delete text-blue\" @click=\"tableDelete('{ item.table }' ,tableIndex)\">");
                            add("            删除");
                            add("          </view>");
                            add("        </view>");
                            add("");

                            item.fieldsData.ForEach(field => {
                                switch (field.type)
                                {
                                    case "label":
                                        add($"        <l-label title=\"{ field.name }\"></l-label>");
                                        break;

                                    case "input":
                                        add($"        <l-input");
                                        add($"          @input=\"setValue(`{ item.table }.${{tableIndex}}.{ field.field }`, $event)\"");
                                        add($"          :value=\"getValue(`{ item.table }.${{tableIndex}}.{ field.field }`)\"");
                                        add($"          :disabled=\"!edit\"");
                                        add($"          title=\"{ field.name }\"");
                                        add($"          placeholder=\"请输入{ field.name }\"");
                                        add($"          right");
                                        add($"        />");
                                        break;

                                    case "radio":
                                    case "select":
                                        add($"        <l-select");
                                        add($"          @input=\"setValue(`{ item.table }.${{tableIndex}}.{ field.field }`, $event)\"");
                                        add($"          :value=\"getValue(`{ item.table }.${{tableIndex}}.{ field.field }`)\"");
                                        add($"          :disabled=\"!edit\"");
                                        add($"          :arrow=\"edit\"");
                                        add($"          :range=\"dataSource.{ item.table }.{ field.field }\"");
                                        add($"          title=\"{ field.name }\"");
                                        add($"          placeholder=\"请选择{ field.name }\"");
                                        add($"        />");
                                        break;

                                    case "checkbox":
                                        add($"        <l-checkbox-picker");
                                        add($"          @open=\"modal = 'checkbox'\"");
                                        add($"          @close=\"modal = null\"");
                                        add($"          @input=\"setValue(`{ item.table }.${{tableIndex}}.{ field.field }`, $event)\"");
                                        add($"          :value=\"getValue(`{ item.table }.${{tableIndex}}.{ field.field }`)\"");
                                        add($"          :readonly=\"!edit\"");
                                        add($"          :arrow=\"edit\"");
                                        add($"          :range=\"dataSource.{ item.table }.{ field.field }\"");
                                        add($"          title=\"{ field.name }\"");
                                        add($"          placeholder=\"请选择{ field.name }\"");
                                        add($"        />");
                                        break;

                                    case "layer":
                                        add($"        <l-layer-picker");
                                        add($"          @input=\"setValue(`{ item.table }.${{tableIndex}}.{ field.field }`, $event)\"");
                                        add($"          :value=\"getValue(`{ item.table }.${{tableIndex}}.{ field.field }`)\"");
                                        add($"          :readonly=\"!edit\"");
                                        add($"          :arrow=\"edit\"");
                                        add($"          :source=\"dataSource.{ item.table }.{ field.field }\"");
                                        add($"          title=\"{ field.name }\"");
                                        add($"          placeholder=\"请选择{ field.name }\"");
                                        add($"        />");
                                        break;

                                    case "datetime":
                                        add($"        <l-date-picker");
                                        add($"          @input=\"setValue(`{ item.table }.${{tableIndex}}.{ field.field }`, $event)\"");
                                        add($"          :value=\"getValue(`{ item.table }.${{tableIndex}}.{ field.field }`)\"");
                                        add($"          :disabled=\"!edit\"");
                                        add($"          :arrow=\"edit\"");
                                        add($"          title=\"{ field.name }\"");
                                        add($"          placeholder=\"请选择{ field.name }\"");
                                        add($"        />");
                                        break;

                                    default: break;
                                }
                            });
                            add($"      </view>");
                            add($"");
                            add($"      <view v-if=\"edit\" @click=\"tableAdd('{ item.table }')\" class=\"bg-white flex flex-wrap justify-center align-center solid-bottom\">");
                            add($"        <view class=\"add-btn text-blue padding\">+ 添加一行表格</view>");
                            add($"      </view>");
                            break;

                        default: break;
                    }
                });
                add("    </view>");
                add("");

                add("    <view v-if=\"ready\" class=\"padding-lr bg-white margin-tb padding-tb\">");
                add("      <l-button v-if=\"edit\" @click=\"action('save')\" class=\"block\" size=\"lg\" color=\"green\" block>");
                add("        提交保存");
                add("      </l-button>");
                add("      <l-button v-if=\"!edit && mode !== 'create'\" @click=\"action('edit')\" class=\"block\" size=\"lg\" line=\"orange\" block>");
                add("        编辑本页");
                add("      </l-button>");
                add("      <l-button v-if=\"edit && mode !== 'create'\" @click=\"action('reset')\" class=\"block margin-top\" size=\"lg\" line=\"red\" block>");
                add("        取消编辑");
                add("      </l-button>");
                add("      <l-button v-if=\"!edit && mode !== 'create'\" @click=\"action('delete')\" class=\"block margin-top\" size=\"lg\" line=\"red\" block>");
                add("        删除");
                add("      </l-button>");
                add("    </view>");
                add("  </view>");
                add("</template>");
                add("");
                add("");

                // JS代码
                add("<script>");
                add("/*");
                add(" * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)");
                add($" * Copyright (c) 2013-{ DateTime.Now.Year } 上海力软信息技术有限公司");
                add(" * 创建人：" + LoginUserInfo.Get()?.realName);
                add(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                add(" * 描  述：" + baseInfo.describe);
                add(" */");
                add("");

                add("import _ from 'lodash'");
                add("import moment from 'moment'");
                add("import { guid, copy } from '@/common/utils.js'");
                add("import customPageMixins from '@/common/custom-page.js'");
                add("");

                add("export default {");
                add("  mixins: [customPageMixins],");
                add("");

                // 构建 scheme 和 dataSource 字符串
                var schemeStr = "";
                var dataSourceStr = "";
                var dataSourceRequestStr = new StringBuilder();
                var syncDataSourceRequestStr = new StringBuilder();
                void dsReqAdd(string str) => dataSourceRequestStr.Append(str).Append("\r\n");
                void syncDsReqAdd(string str) => syncDataSourceRequestStr.Append(str).Append("\r\n");

                string[] needSourceType = { "checkbox", "radio", "select", "layer", "girdtable" };

                var schemesByTable = from item in compontMap.Values group item by item.table into tableGroup select tableGroup;
                foreach (var scheme in schemesByTable)
                {
                    schemeStr += "        " + scheme.Key + ": {\r\n";
                    dataSourceStr += "        " + scheme.Key + ": {\r\n";

                    scheme.ToList().ForEach(item => {
                        // 添加 scheme 信息
                        if (item.type == "girdtable")
                        {
                            schemeStr += "          __GIRDTABLE__: '" + item.title + "',\r\n";
                            item.fieldsData.ForEach(fieldItem => {
                                schemeStr += "          " + fieldItem.field + ": { type: '" + fieldItem.type + "', title: '" + fieldItem.name + "'";
                                if (!string.IsNullOrWhiteSpace(fieldItem.dfvalue)) { schemeStr += $", dfvalue: '{ fieldItem.dfvalue }'"; }
                                if (!string.IsNullOrWhiteSpace(fieldItem.itemCode)) { schemeStr += $", itemCode: '{ fieldItem.itemCode }'"; }
                                if (!string.IsNullOrWhiteSpace(fieldItem.dataSource)) { schemeStr += $", dataSource: '{ fieldItem.dataSource }'"; }
                                if (!string.IsNullOrWhiteSpace(fieldItem.dataSourceId)) { schemeStr += $", dataSourceId: '{ fieldItem.dataSourceId }'"; }
                                if (!string.IsNullOrWhiteSpace(fieldItem.datetime)) { schemeStr += $", dateformat: '{ fieldItem.datetime }'"; }
                                if (fieldItem.layerData != null && !fieldItem.layerData.IsEmpty())
                                {
                                    var layerDataStr = fieldItem.layerData
                                      .FindAll(t => int.Parse(t.hide) != 1 && !string.IsNullOrWhiteSpace(t.value))
                                      .ConvertAll(t => "{ label: '" + t.label + "', name: '" + t.name + "', value: '" + t.value + "' }")
                                      .ToArray();
                                    schemeStr += ", layerData:[" + string.Join(", ", layerDataStr) + "]";
                                }
                                schemeStr += " },\r\n";
                            });
                        }
                        else
                        {
                            schemeStr += "          " + item.field + ": { type: '" + item.type + "', title: '" + item.title + "'";
                            if (!string.IsNullOrWhiteSpace(item.dfvalue)) { schemeStr += $", dfvalue: '{ item.dfvalue }'"; }
                            if (!string.IsNullOrWhiteSpace(item.itemCode)) { schemeStr += $", itemCode: '{ item.itemCode }'"; }
                            if (!string.IsNullOrWhiteSpace(item.dataType)) { schemeStr += $", dataType: '{ item.dataType }'"; }
                            if (!string.IsNullOrWhiteSpace(item.rulecode)) { schemeStr += $", rulecode: '{ item.rulecode }'"; }
                            if (!string.IsNullOrWhiteSpace(item.dataSource)) { schemeStr += $", dataSource: '{ item.dataSource }'"; }
                            if (!string.IsNullOrWhiteSpace(item.dataSourceId)) { schemeStr += $", dataSourceId: '{ item.dataSourceId }'"; }
                            if (!string.IsNullOrWhiteSpace(item.dateformat)) { schemeStr += $", dateformat: '{ item.dateformat }'"; }
                            if (item.type == "datetimerange")
                            {
                                var start = fields.Find(t => t.id == item.startTime);
                                var end = fields.Find(t => t.id == item.endTime);

                                schemeStr += $", startTime: '{ start.table }.{ start.field }'";
                                schemeStr += $", endTime: '{ end.table }.{ end.field }'";
                            }
                            schemeStr += " },\r\n";
                        }

                        // 添加 dataSource 信息以及获取 dataSource 的方法
                        if (item.type == "girdtable")
                        {
                            item.fieldsData?.FindAll(t => needSourceType.Contains(t.type)).ForEach(fieldItem => {
                                var dataSourceItem = fieldItem.type == "layer" ? "{}" : "[]";

                                if (fieldItem.type == "layer")
                                {
                                    // 是 layer 弹层
                                    if (int.Parse(fieldItem.dataSource) == 0)
                                    {
                                        // 来自数据字典
                                        syncDsReqAdd("      (() => {");
                                        syncDsReqAdd("        const source = Object");
                                        syncDsReqAdd("          .values(this.$store.state.propTable." + fieldItem.itemCode + ")");
                                        syncDsReqAdd("          .map(t => ({ value: t.value, text: t.text }))");
                                        syncDsReqAdd("        const selfField = 'value'");
                                        syncDsReqAdd("        const layerData = [{ name: 'text', label: '" + fieldItem.layerData[0].label + "' }, { name: 'value', label: '" + fieldItem.layerData[1].label + "' }]");
                                        syncDsReqAdd("        this.dataSource." + item.table + "." + fieldItem.field + " = { source, selfField, layerData }");
                                        syncDsReqAdd("      })(),");
                                    }
                                    else
                                    {
                                        // 来自数据源
                                        var selfField = fieldItem.layerData?.Find(t => t.value == fieldItem.field)?.name ?? "name";
                                        var layerDataItems = fieldItem.layerData
                                          .FindAll(t => int.Parse(t.hide) != 1 && !string.IsNullOrWhiteSpace(t.value))
                                          .ConvertAll(t => "{ label: '" + t.label + "', name: '" + t.name + "' }")
                                          .ToArray();
                                        var layerDataStr = "[" + string.Join(", ", layerDataItems) + "]";

                                        dsReqAdd("        uni.request({");
                                        dsReqAdd("          url: this.apiRoot + '/datasource/map',");
                                        dsReqAdd("          data: { ...this.auth, data: JSON.stringify({ code:'" + fieldItem.dataSourceId.Split(',')[0] + "', ver: '' }) }");
                                        dsReqAdd("        }).then(([err, { data: { data: sourceData } } = {}]) => {");
                                        dsReqAdd("          if (err || !sourceData || !sourceData.data) { return }");
                                        dsReqAdd("          const source = sourceData.data");
                                        dsReqAdd("          this.dataSource." + item.table + "." + fieldItem.field + " = { source, selfField: '" + selfField + "', layerData: " + layerDataStr + " }");
                                        dsReqAdd("        }),");
                                    }
                                }
                                else
                                {
                                    // 非 layer 弹层
                                    if (int.Parse(fieldItem.dataSource) == 0)
                                    {
                                        // 来自数据字典
                                        dataSourceItem = "Object.values(this.$store.state.propTable." + fieldItem.itemCode + ").map(t => ({ value: t.value, text: t.text }))";
                                    }
                                    else
                                    {
                                        // 来自数据源
                                        dsReqAdd("        uni.request({");
                                        dsReqAdd("          url: this.apiRoot + '/datasource/map',");
                                        dsReqAdd("          data: { ...this.auth, data: JSON.stringify({ code: '" + fieldItem.dataSourceId + "', ver: '' }) }");
                                        dsReqAdd("        }).then(([err, { data: { data: result } } = {}]) => {");
                                        dsReqAdd("          this.dataSource." + item.table + "." + fieldItem.field + " = result.data.map(t => ({ text: t." + fieldItem.showField + ", value: t." + fieldItem.saveField + " }))");
                                        dsReqAdd("        }),");
                                    }
                                }

                                dataSourceStr += $"          { fieldItem.field }: { dataSourceItem },\r\n";
                            });

                        }
                        else if (needSourceType.Contains(item.type))
                        {
                            var dataSourceItem = "[]";

                            if (int.Parse(item.dataSource) == 0)
                            {
                                // 来自数据字典
                                dataSourceItem = "Object.values(this.$store.state.propTable." + item.itemCode + ").map(t => ({ value: t.value, text: t.text }))";
                            }
                            else
                            {
                                // 来自数据源
                                var dataSourceInfo = item?.dataSourceId?.Split(',');
                                if (dataSourceInfo != null && dataSourceInfo.Length >= 3)
                                {
                                    dsReqAdd("        uni.request({");
                                    dsReqAdd("          url: this.apiRoot + '/datasource/map',");
                                    dsReqAdd("          data: { ...this.auth, data: JSON.stringify({ code: '" + dataSourceInfo[0] + "', ver: '' }) }");
                                    dsReqAdd("        }).then(([err, { data: { data: result } } = {}]) => {");
                                    dsReqAdd("          this.dataSource." + item.table + "." + item.field + " = result.data.map(t => ({ text: t." + dataSourceInfo[1] + ", value: t." + dataSourceInfo[2] + " }))");
                                    dsReqAdd("        }),");
                                }
                            }

                            dataSourceStr += $"          { item.field }: { dataSourceItem },\r\n";
                        }
                    });

                    schemeStr += "        },\r\n";
                    dataSourceStr += "        },\r\n";
                }

                // data
                add("  data() {");
                add("    return {");
                add("      id: null,");
                add("      mode: null,");
                add("      edit: null,");
                add("      modal: null,");
                add("      ready: false,");
                add("");

                add("      current: null,");
                add("      origin: null,");
                add("");

                add("      // 表单项数据结构");
                add("      scheme: {");
                add(schemeStr);
                add("      },");
                add("");

                add("      // 数据源");
                add("      dataSource: {");
                add(dataSourceStr);
                add("      }");
                add("    }");
                add("  },");
                add("");

                add("  async onLoad({ type, id }) {");
                add("    await this.init(type, id)");
                add("  },");
                add("");

                // methods
                add("  methods: {");
                add("    async init(type, id) {");
                add("      uni.showLoading({ title: `加载数据中...`, mask: true })");
                add("");
                add("      this.id = id");
                add("      this.mode = type");
                add("      this.edit = ['create', 'edit'].includes(this.mode)");
                add("");
                add(syncDataSourceRequestStr.ToString());
                add("");
                add("      // 加载所有数据源");
                add("      await Promise.all([");
                add(dataSourceRequestStr.ToString());
                add("        this.fetchForm()");
                add("      ])");
                add("");
                add("      this.ready = true");
                add("      uni.hideLoading()");
                add("    },");
                add("");

                add("    // 获取表单值");
                add("    getValue(path) {");
                add("      return _.get(this.current, path)");
                add("    },");
                add("");

                add("    // 设置表单值");
                add("    setValue(path, val) {");
                add("      _.set(this.current, path, val)");
                add("    },");
                add("");

                add("    // 加载表单数据");
                add("    async fetchForm() {");
                add("      if (this.mode === 'create') {");
                add("        this.origin = await this.getDefaultForm()");
                add("      } else {");
                add("        const [err, { data: { data: result } } = {}] = await uni.request({");
                add("          url: this.apiRoot + '" + apiPath + "/form',");
                add("          data: { ...this.auth, data: this.id }");
                add("        })");
                add("        this.origin = this.formatFormData(result)");
                add("      }");
                add("      this.current = copy(this.origin)");
                add("    },");
                add("");

                add("    // 点击「编辑、重置、保存、删除」按钮");
                add("    async action(type) {");
                add("      switch (type) {");
                add("        case 'edit':");
                add("          this.edit = true");
                add("          break");
                add("");
                add("        case 'reset':");
                add("          this.current = copy(this.origin)");
                add("          this.edit = false");
                add("          break");
                add("");
                add("        case 'save':");
                add("          const verifyResult = this.verifyForm()");
                add("          if (verifyResult.length > 0) {");
                add("            uni.showModal({ title: '表单验证失败', content: verifyResult.join('\\n'), showCancel: false })");
                add("            return");
                add("          }");
                add("");
                add("          uni.showModal({");
                add("            title: '提交确认',");
                add("            content: `确定要提交本页表单内容吗？`,");
                add("            success: async ({ confirm }) => {");
                add("              if (!confirm) { return }");
                add("              uni.showLoading({ title: '正在提交...', mask: true })");
                add("              const postData = await this.getPostData()");
                add("");
                add("              uni");
                add("                .request({");
                add("                  url: this.apiRoot + `" + apiPath + "/save`,");
                add("                  method: 'POST',");
                add("                  header: { 'content-type': 'application/x-www-form-urlencoded' },");
                add("                  data: { ...this.auth, data: postData }");
                add("                })");
                add("                .then(([err, result]) => {");
                add("                  uni.hideLoading()");
                add("                  if (err || result.data.code !== 200) {");
                add("                    uni.showToast({ title: `表单提交保存失败`, icon: 'none' })");
                add("                    return");
                add("                  }");
                add("");
                add("                  uni.$emit('" + baseInfo.outputArea + baseInfo.name + "-list-change')");
                add("                  this.origin = copy(this.current)");
                add("                  this.mode = 'view'");
                add("                  this.edit = false");
                add("                  uni.showToast({ title: `提交保存成功`, icon: 'success' })");
                add("                })");
                add("            }");
                add("          })");
                add("          break");
                add("");
                add("        case 'delete':");
                add("          uni.showModal({");
                add("            title: '删除项目',");
                add("            content: `确定要删除本项吗？`,");
                add("            success: ({ confirm }) => {");
                add("              if (!confirm) { return }");
                add("");
                add("              uni");
                add("                .request({");
                add("                  url: this.apiRoot + `" + apiPath + "/delete`,");
                add("                  method: 'POST',");
                add("                  header: { 'content-type': 'application/x-www-form-urlencoded' },");
                add("                  data: { ...this.auth, data: this.id }");
                add("                })");
                add("                .then(([err, { data }]) => {");
                add("                  if (err || !data || data.code !== 200) {");
                add("                    uni.showToast({ title: '删除失败', icon: 'none' })");
                add("                    return");
                add("                  }");
                add("");
                add("                  uni.$emit('" + baseInfo.outputArea + baseInfo.name + "-list-change')");
                add("                  uni.navigateBack()");
                add("                  uni.showToast({ title: '删除成功', icon: 'success' })");
                add("                })");
                add("            }");
                add("          })");
                add("          break");
                add("");
                add("        default: break");
                add("      }");
                add("    }");
                add("  }");
                add("}");
                add("</script>");
                add("");
                add("");

                // LESS 样式
                add("<style lang=\"less\" scoped>");
                add("@import '~@/common/css/custom-form.less';");
                add("</style>");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
