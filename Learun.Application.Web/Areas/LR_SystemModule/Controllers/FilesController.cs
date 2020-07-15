using Learun.Application.Base.Files;
using Learun.Application.Base.SystemModule;
using System.Web.Mvc;
using System;
using Learun.Util;
using Learun.Application.WorkFlow;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary> 
     
     
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件管理 
    /// </summary> 
    public class FilesController : MvcControllerBase
    {
        private FolderIBLL folderIBLL = new FolderBLL();
        private FileInfoIBLL fileInfoIBLL = new FileInfoBLL();
        private FileAuthIBLL fileAuthIBLL = new FileAuthBLL();

        private CodeRuleIBLL codeRuleIBLL = new CodeRuleBLL();

        private NWFProcessIBLL nWFProcessIBLL = new NWFProcessBLL();

        #region 视图功能
        /// <summary>
        /// 文件管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文件发布页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 文件夹管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FolderIndex() {
            return View();
        }

        /// <summary>
        /// 文件夹管理表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FolderForm()
        {
            return View();
        }
        /// <summary>
        /// 文件授权
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileAuthIndex()
        {
            return View();
        }
        /// <summary>
        /// 文件授权
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileAuthFrom()
        {
            return View();
        }

        /// <summary>
        /// 文件授权
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileAuthAddFrom()
        {
            return View();
        }
        /// <summary>
        /// 文件历史
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileHistroyIndex()
        {
            return View();
        }
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取文件夹数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFolderList(string keyWord)
        {
            var data = folderIBLL.GetList(keyWord, null);
            return Success(data);
        }

        /// <summary>
        /// 获取文件夹列表(树结构)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFolderTree()
        {
            var data = folderIBLL.GetTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取字典分类列表(树结构)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFolderEntity(string keyValue)
        {
            var data = folderIBLL.GetEntity(keyValue);
            return this.Success(data);
        }

        [HttpGet]
        
        public ActionResult GetFileInfoByWF(string processId, string fileInfoId)
        {
            var fileListEntity = fileInfoIBLL.GetListEntity(processId);
            if (fileListEntity != null) {
                fileInfoId = fileListEntity.F_FileInfoId;
            }

            string ver = "V1.0";
            FileBInfoEntity fileInfoEntity = null;
            string code;
            if (string.IsNullOrEmpty(fileInfoId))
            {
                code = codeRuleIBLL.GetBillCode("FS01");
            }
            else
            {
                fileInfoEntity = fileInfoIBLL.GetEntity(fileInfoId);
                code = fileInfoEntity.F_Code;
                if (fileListEntity == null)
                {
                    var fileListEntity2 = fileInfoIBLL.GetListEntityByInfoId(fileInfoId);
                    if (fileListEntity2 != null)
                    {
                        string[] verList = fileListEntity2.F_Ver.Split('.');
                        var l = verList.Length;
                        ver = "";
                        for (var i = 0; i < l; i++)
                        {
                            if (i < l - 1)
                            {
                                ver += verList[i] + ".";
                            }
                            else
                            {
                                ver += (Convert.ToInt32(verList[i]) + 1);
                            }
                        }
                    }
                }
                else {
                    ver = fileListEntity.F_Ver;
                }
              
            }

            var jsondata = new {
                ver,
                code,
                fileInfoEntity,
                fileListEntity
            };
            
            return this.Success(jsondata);
        }


        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyWord">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetWFPageList(string pagination, string keyWord, string wfType)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            IEnumerable<WFFileModel> list = new List<WFFileModel>();
            UserInfo userInfo = LoginUserInfo.Get();
            string strSql = "";


            switch (wfType)
            {
                case "1":// 我的流程
                    strSql = nWFProcessIBLL.GetMySql();
                    break;
                case "2":// 待办流程
                    strSql = nWFProcessIBLL.GetMyTaskSql(userInfo);
                    break;
                case "3":// 已办流程
                    strSql = nWFProcessIBLL.GetMyFinishTaskSql();
                    break;
            }

            list = fileInfoIBLL.GetWfPageList(strSql, paginationobj, keyWord, userInfo.userId);

            var jsonData = new
            {
                rows = list,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }


        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyWord">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetAllPublishPageList(string keyWord, string folderId)
        {
            var list = fileInfoIBLL.GetAllPublishPageList(keyWord, folderId);
            return Success(list);
        }
        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="keyWord">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPublishList(string keyWord, string folderId)
        {
            var list = fileInfoIBLL.GetPublishList(keyWord, folderId);
            return Success(list);
        }

        /// <summary>
        /// 文件的历史信息
        /// </summary>
        /// <param name="fileInfoId">文件夹Id</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetHistoryList(string fileInfoId)
        {
            var list = fileInfoIBLL.GetHistoryList(fileInfoId);
            return Success(list);
        }

        /// <summary>
        /// 被删除的文件信息
        /// </summary>
        /// <param name="fileInfoId">文件夹Id</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetDeleteList(string fileInfoId)
        {
            var list = fileInfoIBLL.GetDeleteList(fileInfoId);
            return Success(list);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        
        public ActionResult SaveFolder(string keyValue,FolderEntity entity)
        {
            folderIBLL.SaveEntity(keyValue,entity);
            return this.Success("保存成功");
        }
        [HttpPost]
        
        public ActionResult DeleteFolder(string keyValue)
        {
            var res = folderIBLL.DeleteEntity(keyValue);
            if (res)
            {
                return this.Success("删除成功");
            }
            else {
                return this.Success("不准删除，有文件或子文件夹");
            }
        }

        /// <summary>
        /// 虚拟删除文件
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult VDeleteFile(string keyValue)
        {
            fileInfoIBLL.VDeleteEntity(keyValue);
            return this.Success("删除成功");
        }
        /// <summary>
        /// 还原虚拟删除文件
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult RecoveryFile(string keyValue)
        {
            fileInfoIBLL.RecoveryEntity(keyValue);
            return this.Success("删除成功");
        }
        /// <summary>
        /// 彻底删除文件
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteFile(string keyValue)
        {
            fileInfoIBLL.DeleteEntity(keyValue);
            return this.Success("删除成功");
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strInfoEntity">文件信息</param>
        /// <param name="strListEntity">文件列表信息</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult SaveFile(string keyValue, string strInfoEntity, string strListEntity)
        {
            FileBInfoEntity fileBInfoEntity = strInfoEntity.ToObject<FileBInfoEntity>();
            FilelistEntity filelistEntity = strListEntity.ToObject<FilelistEntity>();
            if (string.IsNullOrEmpty(fileBInfoEntity.F_Id))
            {
                codeRuleIBLL.UseRuleSeed("FS01");
            }
            fileInfoIBLL.SaveEntity(keyValue, fileBInfoEntity, filelistEntity);
          
            return this.Success("保存成功");
        }

        #endregion

        #region 权限管理
        /// <summary>
        /// 判断文件有没有上传权限
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public ActionResult IsUPLoad(string folderId) {
            var res = fileAuthIBLL.IsUPLoad(folderId);
            return Success(res);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="fileInfoId">文件信息主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetAuthList(string fileInfoId)
        {
            var list = fileAuthIBLL.GetList(fileInfoId);
            return Success(list);
        }
        [HttpGet]
        
        public ActionResult GetAuthEntity(string keyValue)
        {
            var list = fileAuthIBLL.GetEntity(keyValue);
            return Success(list);
        }
        
        /// <summary>
        /// 删除授权对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteAuth(string keyValue)
        {
            fileAuthIBLL.DeleteEntity(keyValue);
            return this.Success("删除成功");
        }
        /// <summary>
        /// 保存授权信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult SaveAuth(string keyValue, FileAuthEntity entity)
        {
            var res = fileAuthIBLL.SaveEntity(keyValue, entity);
            if (res)
            {
                return this.Success("保存成功");
            }
            else {
                return this.Success("该角色已经对该文件授权过！");
            }
           
        }

        #endregion
    }
}