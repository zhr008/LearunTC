using Learun.DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件权限管理 
    /// </summary> 
    public class FileAuthService: RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取文件夹权限
        /// </summary>
        /// <param name="id">文件夹id</param>
        /// <returns></returns>
        public IEnumerable<FileAuthEntity> GetFList(string id)
        {
            var strSql = new StringBuilder();

            strSql.Append(" select * from lr_base_fileauth t ");
            strSql.Append(" where F_Type = 0  AND t.F_FileInfoId = @F_FileInfoId ");

            return this.BaseRepository().FindList<FileAuthEntity>(strSql.ToString(), new { F_FileInfoId = id });
        }
        /// <summary>
        /// 获取授权信息列表
        /// </summary>
        /// <param name="F_FileInfoId">文件信息主键</param>
        /// <returns></returns>
        public IEnumerable<FileAuthEntity> GetList(string F_FileInfoId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" select * from lr_base_fileauth t ");
            strSql.Append(" where F_Type = 0  AND t.F_FileInfoId = @F_FileInfoId ");

            return this.BaseRepository().FindList<FileAuthEntity>(strSql.ToString(), new { F_FileInfoId });
        }
        /// <summary>
        /// 实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FileAuthEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<FileAuthEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue) {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId ", new { fromId = keyValue });
                db.Delete<FileAuthEntity>(new FileAuthEntity() { F_Id = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

          
        }
             
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        public bool SaveEntity(string keyValue, FileAuthEntity entity) {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    var entityTmp = db.FindEntity<FileAuthEntity>(" select * from lr_base_fileauth where F_ObjId = @objId AND F_FileInfoId = @Id AND F_Type = 0 ", new { objId = entity.F_ObjId, Id = entity.F_FileInfoId });
                    if (entityTmp != null) {
                        db.Rollback();
                        return false;
                    }
                    entity.Create();
                    db.Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
                }

                db.ExecuteBySql(" delete from lr_base_fileauth where F_from =@fromId ",new { fromId = entity.F_Id });
                int deep = 1;
                if (entity.F_IsFolder == 1)// 文件夹
                {
                    if (entity.F_FileInfoId != "0") {
                        var folderEntity = db.FindEntity<FolderEntity>(entity.F_FileInfoId);
                        SaveSJEntity(folderEntity.F_PId, entity.F_Id, entity, deep, db);
                    }

                    // 授权子文件夹和文件
                    SaveXJEntity(entity.F_FileInfoId, entity.F_Id, entity, deep, db);
                }
                else
                {
                    var fileEntity = db.FindEntity<FileBInfoEntity>(entity.F_FileInfoId);
                    SaveSJEntity(fileEntity.F_Folder, entity.F_Id, entity, deep, db);
                }

                db.Commit();
                return true;
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
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
        private void SaveSJEntity(string folderId,string fromId, FileAuthEntity fromEntity,int deep, IRepository db) {
            if (folderId != "0") {
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
                SaveSJEntity(folderEntity.F_PId, fromId, fromEntity,deep + 1, db);
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
        private void SaveXJEntity(string folderId, string fromId, FileAuthEntity fromEntity,int deep, IRepository db)
        {
            // 获取文件
            var fileList = db.FindList<FileBInfoEntity>(" select * from lr_base_fileinfo where F_IsPublish = 1 AND F_Folder = @folderId ", new { folderId });
            foreach (var item in fileList) {
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
            foreach (var item in folderList) {
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
                SaveXJEntity(item.F_Id, fromId, fromEntity,deep + 1,db);
            }

        }
        #endregion
    }
}
