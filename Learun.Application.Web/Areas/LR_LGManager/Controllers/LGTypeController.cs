using Learun.Application.Language;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:08
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeController : MvcControllerBase
    {
        private LGTypeIBLL lGTypeIBLL = new LGTypeBLL();

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
        
        public ActionResult GetList(string queryJson)
        {
            var data = lGTypeIBLL.GetList(queryJson);
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
            var data = lGTypeIBLL.GetPageList(paginationobj, queryJson);
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
            var data = lGTypeIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetEntityByCode(string keyValue)
        {
            var data = lGTypeIBLL.GetEntityByCode(keyValue);
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
            lGTypeIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, LGTypeEntity entity)
        {
            lGTypeIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 设为主语言
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult SetMainLG(string keyValue)
        {
            lGTypeIBLL.SetMainLG(keyValue);
            return Success("保存成功！");
        }
        #endregion
    }
}