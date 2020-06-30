using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件夹管理 
    /// </summary> 
    public class FolderBLL : FolderIBLL
    {
        private FolderService folderService = new FolderService();

        #region 获取数据 
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="pId">父级id</param>
        /// <returns></returns>
        public IEnumerable<FolderEntity> GetList(string keyWord, string pId)
        {
            try
            {
                return folderService.GetList(keyWord, pId);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetTree() {
            var  list = (List<FolderEntity>)GetList("","");
            list.Add(new FolderEntity()
            {
                F_Id = "0",
                F_PId = "00000",
                F_Name = "主目录"
            });

            UserInfo userInfo = LoginUserInfo.Get();
            if (!userInfo.isSystem)
            {
                string roleIds = userInfo.roleIds;
                if (string.IsNullOrEmpty(roleIds))
                {
                    return new List<TreeModel>();
                }
                else
                {
                    roleIds = "('" + roleIds.Replace(",", "','") + "')";
                    var authList = (List<FileAuthEntity>)folderService.BaseRepository().FindList<FileAuthEntity>(" select * from lr_base_fileauth where F_ObjId in " + roleIds + " AND  F_Time >= @ftime ORDER BY F_Type,F_Level ", new { ftime = DateTime.Now });
                    List<FolderEntity> list2 = new List<FolderEntity>();
                    foreach (var item in list)
                    {
                        item.F_AuthType = "1";
                        var roleIdList = userInfo.roleIds.Split(',');
                        foreach (var roleIdItem in roleIdList)
                        {
                            var authList2 = authList.FindAll(t => t.F_FileInfoId == item.F_Id && t.F_ObjId == roleIdItem);
                            if (authList2.Count > 0)
                            {
                                if (authList2[0].F_Type != 2 && authList2[0].F_AuthType.IndexOf("2") != -1)
                                {
                                    item.F_AuthType = "2";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            


            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_Id;
                node.text = item.F_Name;
                node.value = item.F_Id;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_PId;
                node.checkstate = 2; //1表示没有权限，2表示有权限

                if (!userInfo.isSystem) {
                    if (item.F_AuthType == "2")
                    {
                        node.checkstate = 2;
                    }
                    else {
                        node.checkstate = 1;
                        node.text += "【无权限】";
                    }
                }

                treeList.Add(node);
            }
            return treeList.ToTree();
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
                return folderService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        public bool DeleteEntity(string keyValue)
        {
            try
            {
                return folderService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
            try
            {
                folderService.SaveEntity(keyValue, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion
    }
}
