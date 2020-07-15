using Learun.Application.Base.SystemModule;
using Learun.Application.Extention.DisplayBoardManage;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:08
    /// 描 述：看板发布
    /// </summary>
    public class LR_KBFeaManageController : MvcControllerBase
    {
        private LR_KBFeaManageIBLL lR_KBFeaManageIBLL = new LR_KBFeaManageBLL();
        private ModuleIBLL moduleIBLL = new ModuleBLL();
        #region 视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetList( string queryJson )
        {
            var data = lR_KBFeaManageIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = lR_KBFeaManageIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFormData(string keyValue)
        {
            var data = lR_KBFeaManageIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteForm(string keyValue)
        {
            string moduleId = lR_KBFeaManageIBLL.GetEntity(keyValue).F_ModuleId;
            lR_KBFeaManageIBLL.DeleteEntity(keyValue);
            moduleIBLL.Delete(moduleId);

            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue,LR_KBFeaManageEntity entity)
        {
            try
            {
                ModuleEntity moduleEntity = new ModuleEntity();
                if (string.IsNullOrEmpty(keyValue))// 新增
                {
                    entity.Create();          
                    moduleEntity.F_Target = "iframe";  
                }
                moduleEntity.F_UrlAddress = "/LR_DisplayBoard/LR_KBKanBanInfo/PreviewForm?keyValue=" + entity.F_KanBanId;
                moduleEntity.F_ModuleId = entity.F_ModuleId;
                moduleEntity.F_ParentId = entity.F_ParentId;
                moduleEntity.F_Icon = entity.F_Icon;
                moduleEntity.F_FullName = entity.F_FullName;
                moduleEntity.F_EnCode = entity.F_EnCode;
                moduleEntity.F_SortCode = entity.F_SortCode;
                moduleEntity.F_IsMenu = 1;
                moduleEntity.F_EnabledMark = 1;
                List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
                ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
                List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
                List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();
                moduleIBLL.SaveEntity(moduleEntity.F_ModuleId, moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);

                entity.F_ModuleId = moduleEntity.F_ModuleId;
                lR_KBFeaManageIBLL.SaveEntity(keyValue, entity);
                return Success("保存成功！");
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
