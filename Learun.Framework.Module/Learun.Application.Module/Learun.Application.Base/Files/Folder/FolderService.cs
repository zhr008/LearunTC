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
    /// 描 述：文件夹管理 
    /// </summary> 
    public class FolderService : RepositoryFactory
    {
        #region 获取数据 
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="pId">父级id</param>
        /// <returns></returns>
        public IEnumerable<FolderEntity> GetList(string keyWord,string pId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_Id, 
                t.F_PId,
                t.F_Name,
                t.F_Time
                ");
                strSql.Append("  FROM lr_base_folder t ");
                strSql.Append("  WHERE 1=1  ");
                if (!string.IsNullOrEmpty(pId)) {
                    strSql.Append(" AND t.F_PId = @pId ");
                }


                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = "%" + keyWord + "%";
                    strSql.Append(" AND t.F_Name Like @keyWord ");
                }
                strSql.Append(" Order by F_Name ");

                return this.BaseRepository().FindList<FolderEntity>(strSql.ToString(), new { keyWord, pId });
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
        /// 获取lr_base_folder表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public FolderEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FolderEntity>(keyValue);
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
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool DeleteEntity(string keyValue)
        {
            var fileList = (List<FileBInfoEntity>)this.BaseRepository().FindList<FileBInfoEntity>(" select * from lr_base_fileinfo where F_IsPublish = 1 AND F_Folder = @folderId ", new { folderId = keyValue });
            if (fileList.Count > 0)
            {
                return false;
            }
            var folderList = (List<FolderEntity>)this.BaseRepository().FindList<FolderEntity>(" select * from lr_base_folder where  F_PId = @folderId ", new { folderId = keyValue });
            if (fileList.Count > 0)
            {
                return false;
            }

            var authList =  this.BaseRepository().FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId ", new { F_FileInfoId = keyValue });

            var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (var item in authList) {
                    db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId ", new { fromId = item.F_Id });
                }
                db.Delete<FolderEntity>(t => t.F_Id == keyValue);
                db.Commit();
                return true;
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
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        public void SaveEntity(string keyValue, FolderEntity entity)
        {
         
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);

                    var entityTmp = db.FindEntity<FolderEntity>("select * from lr_base_folder where F_Id = @keyValue",new { keyValue });
                    if (entityTmp.F_PId != entity.F_PId) {// 修改了上级了目录需要调整权限
                        if (entityTmp.F_PId != "0") { // 需要删除原先继承过来的权限
                            var authList = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId ", new { F_FileInfoId = keyValue });
                            foreach (var item in authList)
                            {
                                if (item.F_Type == 1)// 来自上级文件夹的需要删除
                                {
                                    db.ExecuteBySql(" delete from lr_base_fileauth where F_Id =@id ", new { id = item.F_Id });
                                    db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId AND F_Level > @deep AND F_Type = 1 ", new { fromId = item.F_from, deep = item.F_Level });
                                }
                                else {
                                    if (item.F_Type == 0)
                                    {
                                        item.F_from = item.F_Id;
                                    }
                                    db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId AND F_Level > @deep AND F_Type = 2 ", new { fromId = item.F_from, deep = item.F_Level });
                                }
                            }
                        }

                        if (entity.F_PId != "0") { // 添加新的权限
                            var authList2 = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId AND F_Type != 2 ", new { F_FileInfoId = entity.F_PId });
                            foreach (var item in authList2)
                            {
                                if (item.F_Type == 0)
                                {
                                    item.F_from = item.F_Id;
                                }
                                FileAuthEntity authEntity = new FileAuthEntity()
                                {
                                    F_Id = Guid.NewGuid().ToString(),
                                    F_AuthType = "1",
                                    F_FileInfoId = entity.F_Id,
                                    F_from = item.F_from,
                                    F_ObjId = item.F_ObjId,
                                    F_ObjName = item.F_ObjName,
                                    F_ObjType = item.F_ObjType,
                                    F_Time = item.F_Time,
                                    F_Type = 1,
                                    F_IsFolder = 1,
                                    F_Level = item.F_Level + 1
                                };
                                db.Insert(authEntity);
                                SaveXJEntity(entity.F_Id, item.F_from, item, authEntity.F_Level + 1, db);
                            }

                            // 将自己的权限赋值给上级目录
                            var authList3 = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId  AND F_Type != 1 ", new { F_FileInfoId = entity.F_Id });
                            foreach (var item in authList3)
                            {
                                if (item.F_Type == 0)
                                {
                                    item.F_from = item.F_Id;
                                }
                                SaveSJEntity(entity.F_PId, item.F_from, item, item.F_Level + 1, db);
                            }


                        }
                    }

                   
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);

                    if (entity.F_PId != "0") {
                        // 继承上级文件夹的权限
                        var authList = db.FindList<FileAuthEntity>(" select * from lr_base_fileauth t where t.F_FileInfoId = @F_FileInfoId ", new { F_FileInfoId = entity.F_PId });
                        foreach(var item in authList) {
                            if (item.F_Type != 2) {
                                if (item.F_Type == 0) {
                                    item.F_from = item.F_Id;
                                }
                                FileAuthEntity authEntity = new FileAuthEntity()
                                {
                                    F_Id = Guid.NewGuid().ToString(),
                                    F_AuthType = "1",
                                    F_FileInfoId = entity.F_Id,
                                    F_from = item.F_from,
                                    F_ObjId = item.F_ObjId,
                                    F_ObjName = item.F_ObjName,
                                    F_ObjType = item.F_ObjType,
                                    F_Time = item.F_Time,
                                    F_Type = 1,
                                    F_IsFolder = 1,
                                    F_Level = item.F_Level + 1
                                };
                                db.Insert(authEntity);
                            }
                        }
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


        /// <summary>
        /// 保存上级目录权限设置
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="fromId"></param>
        /// <param name="fromEntity"></param>
        /// <param name="deep"></param>
        /// <param name="db"></param>
        private void SaveSJEntity(string folderId, string fromId, FileAuthEntity fromEntity, int deep, IRepository db)
        {
            if (folderId != "0")
            {
                FileAuthEntity entity = new FileAuthEntity()
                {
                    F_Id = Guid.NewGuid().ToString(),
                    F_AuthType = "1",
                    F_FileInfoId = folderId,
                    F_from = fromId,
                    F_ObjId = fromEntity.F_ObjId,
                    F_ObjName = fromEntity.F_ObjName,
                    F_ObjType = fromEntity.F_ObjType,
                    F_Time = fromEntity.F_Time,
                    F_Type = 2,
                    F_IsFolder = 1,
                    F_Level = deep
                };
                db.Insert(entity);
                var folderEntity = db.FindEntity<FolderEntity>(folderId);
                SaveSJEntity(folderEntity.F_PId, fromId, fromEntity, deep + 1, db);
            }
        }

        /// <summary>
        /// 保存下级目录权限设置
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="fromId"></param>
        /// <param name="fromEntity"></param>
        /// <param name="deep"></param>
        /// <param name="db"></param>
        private void SaveXJEntity(string folderId, string fromId, FileAuthEntity fromEntity, int deep, IRepository db)
        {
            // 获取文件
            var fileList = db.FindList<FileBInfoEntity>(" select * from lr_base_fileinfo where F_IsPublish = 1 AND F_Folder = @folderId ", new { folderId });
            foreach (var item in fileList)
            {
                FileAuthEntity fileAuth = new FileAuthEntity()
                {
                    F_Id = Guid.NewGuid().ToString(),
                    F_AuthType = fromEntity.F_AuthType,
                    F_FileInfoId = item.F_Id,
                    F_from = fromId,
                    F_ObjId = fromEntity.F_ObjId,
                    F_ObjName = fromEntity.F_ObjName,
                    F_ObjType = fromEntity.F_ObjType,
                    F_Time = fromEntity.F_Time,
                    F_Type = 1,
                    F_IsFolder = 0,
                    F_Level = deep
                };
                db.Insert(fileAuth);
            }

            // 获取文件夹
            var folderList = db.FindList<FolderEntity>(" select * from lr_base_folder where  F_PId = @folderId ", new { folderId });
            foreach (var item in folderList)
            {
                FileAuthEntity folderAuth = new FileAuthEntity()
                {
                    F_Id = Guid.NewGuid().ToString(),
                    F_AuthType = fromEntity.F_AuthType,
                    F_FileInfoId = item.F_Id,
                    F_from = fromId,
                    F_ObjId = fromEntity.F_ObjId,
                    F_ObjName = fromEntity.F_ObjName,
                    F_ObjType = fromEntity.F_ObjType,
                    F_Time = fromEntity.F_Time,
                    F_Type = 1,
                    F_IsFolder = 1,
                    F_Level = deep
                };
                db.Insert(folderAuth);
                SaveXJEntity(item.F_Id, fromId, fromEntity, deep + 1, db);
            }

        }

        #endregion
    }
}
