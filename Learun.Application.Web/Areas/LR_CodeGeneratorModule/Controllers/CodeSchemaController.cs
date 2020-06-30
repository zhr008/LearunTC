using Learun.Util;
using System.Data;
using Learun.Application.Base.CodeSchemaModule;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_CodeGeneratorModule.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-03-01 11:09
    /// 描 述：代码模板
    /// </summary>
    public class CodeSchemaController : MvcControllerBase
    {
        private CodeSchemaIBLL codeSchemaIBLL = new CodeSchemaBLL();

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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = codeSchemaIBLL.GetPageList(paginationobj, queryJson);
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
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var LR_Base_CodeSchemaData = codeSchemaIBLL.GetLR_Base_CodeSchemaEntity( keyValue );
            var jsonData = new {
                LR_Base_CodeSchema = LR_Base_CodeSchemaData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取左侧树形数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTree()
        {
            var data = codeSchemaIBLL.GetTree();
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
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            codeSchemaIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            LR_Base_CodeSchemaEntity entity = strEntity.ToObject<LR_Base_CodeSchemaEntity>();
            codeSchemaIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
