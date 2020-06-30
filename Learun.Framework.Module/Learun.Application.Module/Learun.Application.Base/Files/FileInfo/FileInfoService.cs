using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件管理 
    /// </summary> 
    public class FileInfoService: RepositoryFactory
    {
        #region 获取数据

        /// <summary> 
        /// 获取表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public FileBInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FileBInfoEntity>(keyValue);
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
        /// 获取文件列表实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FilelistEntity GetListEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FilelistEntity>(keyValue);
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
        /// 获取文件列表实体
        /// </summary>
        /// <param name="fileInfoId"></param>
        /// <returns></returns>
        public FilelistEntity GetListEntityByInfoId(string fileInfoId)
        {
            try
            {
                return this.BaseRepository().FindEntity<FilelistEntity>(" select * from lr_base_filelist where F_FileInfoId = @fileInfoId  AND F_IsPublish = 1 ", new { fileInfoId });
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
        /// 文件审核列表
        /// </summary>
        /// <param name="strWfSql">查询语句</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键字</param>
        /// <param name="userId">当前用户Id</param>
        /// <returns></returns>
        public IEnumerable<WFFileModel> GetWfPageList(string strWfSql, Pagination pagination, string keyword,string userId) {
            var strSql = new StringBuilder();
            strSql.Append("select t.*,l.F_Ver as FileVer,l.F_Name as FileName ,f.F_Code as FileCode from (" + strWfSql  + ")t");
            strSql.Append(" LEFT JOIN lr_base_filelist l on l.F_Id = t.F_Id ");
            strSql.Append(" LEFT JOIN lr_base_fileinfo f on f.F_Id = l.F_FileInfoId where 1=1 AND t.F_SchemeCode = 'lr_files_manager' ");

            if (!string.IsNullOrEmpty(keyword)) {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND f.F_Name like @keyword ");
            }


            return this.BaseRepository().FindList<WFFileModel>(strSql.ToString(),new { keyword, userId }, pagination);
        }

        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="folderId">分页参数</param>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetAllPublishPageList(string keyword, string folderId)
        {
            List<FileBInfoEntity> list = new List<FileBInfoEntity>();

            if (string.IsNullOrEmpty(keyword))
            {
                list.AddRange(GetFolderList(keyword, folderId));
            }

            var strSql = new StringBuilder();
            strSql.Append(" select t.*,t1.F_Ver,t1.F_FileId,t1.F_PFiled,t3.F_FileSize,t3.F_FileType,1 as Type,'1,2,3,4,5,6' as F_AuthType from lr_base_fileinfo t ");
            strSql.Append(" LEFT JOIN lr_base_filelist t1 on t1.F_FileInfoId = t.F_Id ");
            //strSql.Append(" LEFT JOIN lr_base_fileauth t2 on t2.F_FileInfoId = t.F_Id ");
            strSql.Append(" LEFT JOIN LR_Base_AnnexesFile t3 on t3.F_FolderId = t1.F_FileId ");
            strSql.Append(" where t.F_DeleteMark = 0 AND t.F_IsPublish = 1 AND t1.F_IsPublish = 1 ");



            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND (t.F_Name like @keyword OR t.F_KeyWord like @keyword ) ");
            }
            else
            {
                strSql.Append(" AND t.F_Folder = @folderId ");
            }
            list.AddRange(this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { keyword, folderId }));

            return list;
        }



        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="folderId">父级id</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetFolderList(string keyWord, string folderId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_PId as F_Folder,
                t.F_Name,
                2 as Type,
                'folder' as F_FileType
                ");
            strSql.Append("  FROM lr_base_folder t ");
            strSql.Append("  WHERE 1=1  ");

            if (!string.IsNullOrEmpty(folderId))
            {
                strSql.Append(" AND t.F_PId = @folderId ");
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = "%" + keyWord + "%";
                strSql.Append(" AND t.F_Name Like @keyWord ");
            }
            strSql.Append(" Order by F_Name ");

            return this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { keyWord, folderId });
        }



       /// <summary>
       /// 获取文件夹带权限的
       /// </summary>
       /// <param name="keyWord"></param>
       /// <param name="folderId"></param>
       /// <param name="authList"></param>
       /// <param name="userInfo"></param>
       /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetFolderList2(string keyWord, string folderId, List<FileAuthEntity> authList, UserInfo userInfo)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id,
                t.F_PId as F_Folder,
                t.F_Name,
                2 as Type,
                '2' as F_AuthType,
                'folder' as F_FileType
                ");
            strSql.Append("  FROM lr_base_folder t ");
            strSql.Append("  WHERE 1=1  ");

            if (!string.IsNullOrEmpty(folderId))
            {
                strSql.Append(" AND t.F_PId = @folderId ");
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = "%" + keyWord + "%";
                strSql.Append(" AND t.F_Name Like @keyWord ");
            }
            strSql.Append(" Order by F_Name ");

            var list =this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { keyWord, folderId });

            if (userInfo.isSystem)
            {
                return list;
            }
            else {
                List<FileBInfoEntity> list2 = new List<FileBInfoEntity>();
                foreach (var item in list) {
                    item.F_AuthType = "1";
                    var roleIdList = userInfo.roleIds.Split(',');
                    bool flag = false;
                    foreach (var roleIdItem in roleIdList) {
                        var authList2 = authList.FindAll(t => t.F_FileInfoId == item.F_Id && t.F_ObjId == roleIdItem);
                        if (authList2.Count > 0)
                        {
                            flag = true;
                            if (authList2[0].F_Type != 2 && authList2[0].F_AuthType.IndexOf("2") != -1)
                            {
                                item.F_AuthType = "2";
                                break;
                            }
                        }
                    }
                    if (flag) {
                        list2.Add(item);
                    }
                }
                return list2;
            }
        }
        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetPublishList(string keyword, string folderId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            List<FileAuthEntity> authList = new List<FileAuthEntity>();
            List<FileBInfoEntity> list = new List<FileBInfoEntity>();
            if (!userInfo.isSystem)
            {
                string roleIds = userInfo.roleIds;
                if (string.IsNullOrEmpty(roleIds))
                {
                    return new List<FileBInfoEntity>();
                }
                else
                {
                    roleIds = "('" + roleIds.Replace(",", "','") + "')";
                    authList = (List<FileAuthEntity>)this.BaseRepository().FindList<FileAuthEntity>(" select * from lr_base_fileauth where F_ObjId in " + roleIds + " AND  F_Time >= @ftime ORDER BY F_Type,F_Level ", new { ftime = DateTime.Now });
                }
            }



            if (string.IsNullOrEmpty(keyword))
            {
                list.AddRange(GetFolderList2(keyword, folderId, authList, userInfo));
            }

            var strSql = new StringBuilder();
            strSql.Append(" select t.*,t1.F_Ver,t1.F_FileId,t1.F_PFiled,t3.F_FileSize,t3.F_FileType,1 as Type,'1,2,3,4,5,6' as F_AuthType from lr_base_fileinfo t ");
            strSql.Append(" LEFT JOIN lr_base_filelist t1 on t1.F_FileInfoId = t.F_Id ");
            //strSql.Append(" LEFT JOIN lr_base_fileauth t2 on t2.F_FileInfoId = t.F_Id ");
            strSql.Append(" LEFT JOIN LR_Base_AnnexesFile t3 on t3.F_FolderId = t1.F_FileId ");
            strSql.Append(" where t.F_DeleteMark = 0 AND t.F_IsPublish = 1 AND t1.F_IsPublish = 1 ");



            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND (t.F_Name like @keyword OR t.F_KeyWord like @keyword ) ");
            }
            else
            {
                strSql.Append(" AND t.F_Folder = @folderId ");
            }
            var fileList = this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { keyword, folderId });




            if (!userInfo.isSystem)
            {
                foreach (var item in fileList)
                {
                    var roleIdList = userInfo.roleIds.Split(',');
                    bool flag = false;
                    item.F_AuthType = "";
                    foreach (var roleIdItem in roleIdList)
                    {
                        var authList2 = authList.FindAll(t => t.F_FileInfoId == item.F_Id && t.F_ObjId == roleIdItem);
                        if (authList2.Count > 0)
                        {
                            flag = true;
                            if (item.F_AuthType != "")
                            {
                                item.F_AuthType += ",";
                            }
                            item.F_AuthType += authList2[0].F_AuthType;
                        }
                    }

                    if (flag)
                    {
                        list.Add(item);
                    }
                }
            }
            else {
                list.AddRange(fileList);
            }
            return list;
        }

        /// <summary>
        /// 获取文件的历史信息
        /// </summary>
        /// <param name="fileInfoId">主键</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetHistoryList(string fileInfoId)
        {
            List<FileBInfoEntity> list = new List<FileBInfoEntity>();


            var strSql = new StringBuilder();
            strSql.Append(" select t.*,t1.F_Ver,t1.F_FileId,t1.F_PFiled,t3.F_FileSize,t3.F_FileType,1 as Type,'1,3' as F_AuthType from lr_base_fileinfo t ");
            strSql.Append(" LEFT JOIN lr_base_filelist t1 on t1.F_FileInfoId = t.F_Id ");
            strSql.Append(" LEFT JOIN LR_Base_AnnexesFile t3 on t3.F_FolderId = t1.F_FileId ");
            strSql.Append(" where t.F_IsPublish = 1 AND t.F_Id = @fileInfoId  order by t1.F_PublishTime DESC ");

            list.AddRange(this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { fileInfoId}));

            UserInfo userInfo = LoginUserInfo.Get();

            if (!userInfo.isSystem)
            {
                string roleIds = userInfo.roleIds;
                if (string.IsNullOrEmpty(roleIds))
                {
                    return list;
                }
                else
                {
                    roleIds = "('" + roleIds.Replace(",", "','") + "')";

                    var authList = (List<FileAuthEntity>)this.BaseRepository().FindList<FileAuthEntity>(" select * from lr_base_fileauth where F_ObjId in " + roleIds + " AND  F_Time >= @ftime  ORDER BY F_Level ", new { ftime = DateTime.Now });


                    List<FileBInfoEntity> list2 = new List<FileBInfoEntity>();

                    foreach (var item in list)
                    {
                        var roleIdList = userInfo.roleIds.Split(',');
                        bool flag = false;
                        item.F_AuthType = "";
                        foreach (var roleIdItem in roleIdList)
                        {
                            var authList2 = authList.FindAll(t => t.F_FileInfoId == item.F_Id && t.F_ObjId == roleIdItem);
                            if (authList2.Count > 0)
                            {
                                flag = true;
                                if (item.F_AuthType != "")
                                {
                                    item.F_AuthType += ",";
                                }
                                item.F_AuthType += authList2[0].F_AuthType;
                            }
                        }

                        if (flag)
                        {
                            list2.Add(item);
                        }
                    }

                    return list2;
                }
            }
            return list;
        }


        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetDeleteList(string keyword)
        {
            List<FileBInfoEntity> list = new List<FileBInfoEntity>();

            var strSql = new StringBuilder();
            strSql.Append(" select t.*,t1.F_Ver,t1.F_FileId,t1.F_PFiled,t3.F_FileSize,t3.F_FileType,1 as Type,'1,2,3,4,5,6' as F_AuthType from lr_base_fileinfo t ");
            strSql.Append(" LEFT JOIN lr_base_filelist t1 on t1.F_FileInfoId = t.F_Id ");
            strSql.Append(" LEFT JOIN LR_Base_AnnexesFile t3 on t3.F_FolderId = t1.F_FileId ");
            strSql.Append(" where t.F_DeleteMark = 1 AND t.F_IsPublish = 1 AND t1.F_IsPublish = 1 ");

            list.AddRange(this.BaseRepository().FindList<FileBInfoEntity>(strSql.ToString(), new { keyword }));

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND t.F_Name like @keyword ");
            }


            UserInfo userInfo = LoginUserInfo.Get();

            if (!userInfo.isSystem)
            {
                string postIds = userInfo.postIds;
                if (string.IsNullOrEmpty(postIds))
                {
                    return list;
                }
                else
                {
                    postIds = "('" + postIds.Replace(",", "','") + "')";

                    var authList = (List<FileAuthEntity>)this.BaseRepository().FindList<FileAuthEntity>(" select * from lr_base_fileauth where F_ObjId in " + postIds + " AND  F_Time <= ftime ", new { ftime = DateTime.Now });


                    List<FileBInfoEntity> list2 = new List<FileBInfoEntity>();

                    foreach (var item in list)
                    {
                        var fileList = authList.FindAll(t => t.F_FileInfoId == item.F_Id);
                        if (fileList.Count > 0)
                        {
                            string authType = "";
                            foreach (var fileItem in fileList)
                            {
                                if (authType != "")
                                {
                                    authType += ",";
                                }
                                authType += fileItem.F_AuthType;
                            }
                            item.F_AuthType = authType;
                            list2.Add(item);
                        }
                    }

                    return list2;
                    //strSql.Append(" AND t2.F_ObjId in " + postIds);
                }
            }






            return list;
        }
        #endregion


        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">文件列表主键</param>
        /// <param name="fileBInfoEntity">文件主信息</param>
        /// <param name="filelistEntity">文件列表信息</param>
        public void SaveEntity(string keyValue, FileBInfoEntity fileBInfoEntity, FilelistEntity filelistEntity) {
            var db = this.BaseRepository().BeginTrans();

            try
            {
                if (string.IsNullOrEmpty(fileBInfoEntity.F_Id))
                {
                    fileBInfoEntity.Create();
                    db.Insert(fileBInfoEntity);
                }
                else {
                    db.Update(fileBInfoEntity);
                }
                filelistEntity.F_FileInfoId = fileBInfoEntity.F_Id;
                if (string.IsNullOrEmpty(keyValue))
                {
                    filelistEntity.Create();
                   
                    db.Insert(filelistEntity);
                }
                else {
                    filelistEntity.Modify(keyValue);
                    db.Update(filelistEntity);
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

        /// <summary>
        /// 更新文件发布状态
        /// </summary>
        /// <param name="processId"></param>
        public void UpdateEntity(string processId) {
            var fileList = this.BaseRepository().FindEntity<FilelistEntity>(processId);

            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.ExecuteBySql(" update lr_base_filelist set F_IsPublish = 0 where F_FileInfoId=@id ",new {id = fileList.F_FileInfoId });

                FilelistEntity filelistEntity = new FilelistEntity()
                {
                    F_Id = fileList.F_Id,
                    F_IsPublish = 1,
                    F_PublishTime = DateTime.Now
                };
                FileBInfoEntity fileBInfoEntity = new FileBInfoEntity
                {
                    F_Id = fileList.F_FileInfoId,
                    F_IsPublish = 1,
                    F_DeleteMark = 0,
                    F_KeyWord = fileList.F_KeyWord,
                    F_Name = fileList.F_Name,
                    F_Folder = fileList.F_Folder
                };

                db.Update(filelistEntity);
                db.Update(fileBInfoEntity);

                // 更新权限
                // 删除上从层文件夹继承的权限
                var authList = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId ", new { F_FileInfoId = fileList.F_FileInfoId });
                foreach (var item in authList) {
                    if (item.F_Type != 0)
                    {
                        db.ExecuteBySql(" delete from lr_base_fileauth where F_Id =@id", new { id = item.F_Id });
                    }
                    else {
                        db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId ", new { fromId = item.F_Id});
                    }
                }
                // 添加
                var authList2 = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId AND F_Type != 2 ", new { F_FileInfoId = fileList.F_Folder });
                foreach (var item in authList2)
                {
                    if (item.F_Type == 0)
                    {
                        item.F_from = item.F_Id;
                    }
                    FileAuthEntity authEntity = new FileAuthEntity()
                    {
                        F_Id = Guid.NewGuid().ToString(),
                        F_AuthType = item.F_AuthType,
                        F_FileInfoId = fileList.F_FileInfoId,
                        F_from = item.F_from,
                        F_ObjId = item.F_ObjId,
                        F_ObjName = item.F_ObjName,
                        F_ObjType = item.F_ObjType,
                        F_Time = item.F_Time,
                        F_Type = 1,
                        F_IsFolder = 0,
                        F_Level = item.F_Level + 1
                    };
                    db.Insert(authEntity);
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

        /// <summary>
        /// 虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VDeleteEntity(string keyValue) {
            FileBInfoEntity entity = new FileBInfoEntity
            {
                F_Id = keyValue,
                F_DeleteMark = 1
            };
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 还原虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RecoveryEntity(string keyValue)
        {
            FileBInfoEntity entity = new FileBInfoEntity
            {
                F_Id = keyValue,
                F_DeleteMark = 0
            };
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntity(string keyValue) {
            var list = this.BaseRepository().FindList<FilelistEntity>(" select * from lr_base_filelist where F_FileInfoId = @fileInfoId", new { fileInfoId= keyValue });
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<FileBInfoEntity>(new FileBInfoEntity { F_Id = keyValue });
                
                db.ExecuteBySql(" Delete from lr_base_filelist where F_FileInfoId = @f_id ", new { f_id = keyValue });
                foreach (var item in list) {
                    db.ExecuteBySql(" Delete from lr_nwf_process where F_Id = @f_id ", new { f_id = item.F_Id });
                    db.ExecuteBySql(" Delete from lr_nwf_task where F_ProcessId = @f_id ", new { f_id = item.F_Id });
                    db.ExecuteBySql(" Delete from lr_nwf_tasklog where F_ProcessId = @f_id ", new { f_id = item.F_Id });
                    db.ExecuteBySql(" Delete from lr_nwf_taskmsg where F_ProcessId = @f_id ", new { f_id = item.F_Id });
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
