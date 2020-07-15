using Learun.Application.Extention.TaskScheduling;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_TaskScheduling.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-10-19 16:07
    /// 描 述：任务计划模板
    /// </summary>
    public class TSSchemeController : MvcControllerBase
    {
        private TSSchemeIBLL tSSchemeIBLL = new TSSchemeBLL();
        private TSProcessIBLL tSProcessIBLL = new TSProcessBLL();

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
        /// <summary>
        /// 新增明细页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddDetailedForm()
        {
            return View();
        }
        /// <summary>
        /// 查看预置表达式
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectExpressForm()
        {
            return View();
        }
        
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = tSSchemeIBLL.GetPageList(paginationobj, queryJson);
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
            var schemeInfoEntity = tSSchemeIBLL.GetSchemeInfoEntity(keyValue);
            var schemeEntity = tSSchemeIBLL.GetSchemeEntityByInfo(schemeInfoEntity.F_Id);

            var data = new
            {
                schemeInfoEntity,
                schemeEntity
            };

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
            tSSchemeIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue,string strSchemeInfo,string strScheme)
        {
            TSSchemeInfoEntity tSSchemeInfoEntity = strSchemeInfo.ToObject<TSSchemeInfoEntity>();
            TSSchemeEntity tSSchemeEntity = new TSSchemeEntity() {
                F_Scheme = strScheme,
                F_IsActive = 1
            };

            tSSchemeIBLL.SaveEntity(keyValue, tSSchemeInfoEntity, tSSchemeEntity);
            return Success("保存成功！");
        }
        #endregion

        #region 扩展应用
        /// <summary>
        /// 暂停一个任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        /// <returns></returns>
        public ActionResult PauseJob(string processId)
        {
            tSSchemeIBLL.PauseJob(processId);
            return Success("操作成功！");
        }
        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        /// <returns></returns>
        public ActionResult ResumeJob(string processId)
        {
            tSSchemeIBLL.EnAbleJob(processId);
            return Success("操作成功！");
        }
        #endregion
    }
}
