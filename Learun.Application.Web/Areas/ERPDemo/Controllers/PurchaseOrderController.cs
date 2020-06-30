using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.ERPDemo.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 09:37
    /// 描 述：采购订单
    /// </summary>
    public class PurchaseOrderController : MvcControllerBase
    {
        private PurchaseOrderIBLL purchaseOrderIBLL = new PurchaseOrderBLL();

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
        [HttpGet]
        public ActionResult WareForm()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HistoryForm()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Report()
        {
            return View();
        }
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
            var data = purchaseOrderIBLL.GetPageList(paginationobj, queryJson);
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
            var LR_ERP_PurchaseOrderData = purchaseOrderIBLL.GetLR_ERP_PurchaseOrderEntity( keyValue );
            var LR_ERP_PurchaseOrderDetailData = purchaseOrderIBLL.GetLR_ERP_PurchaseOrderDetailList( LR_ERP_PurchaseOrderData.F_Id );
            var jsonData = new {
                LR_ERP_PurchaseOrder = LR_ERP_PurchaseOrderData,
                LR_ERP_PurchaseOrderDetail = LR_ERP_PurchaseOrderDetailData,
            };
            return Success(jsonData);
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
            purchaseOrderIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strlR_ERP_PurchaseOrderDetailList)
        {
            LR_ERP_PurchaseOrderEntity entity = strEntity.ToObject<LR_ERP_PurchaseOrderEntity>();
            List<LR_ERP_PurchaseOrderDetailEntity> lR_ERP_PurchaseOrderDetailList = strlR_ERP_PurchaseOrderDetailList.ToObject<List<LR_ERP_PurchaseOrderDetailEntity>>();
            purchaseOrderIBLL.SaveEntity(keyValue,entity,lR_ERP_PurchaseOrderDetailList);
            if (string.IsNullOrEmpty(keyValue))
            {
            }
            return Success("保存成功！");
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPurchasePageList(string pagination, string purchaseId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = purchaseOrderIBLL.GetPurchasePageList(paginationobj, purchaseId);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult Update(string purchaseInfoId, string purchaseId)
        {
            purchaseOrderIBLL.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
            purchaseOrderIBLL.UpdatePurchase(purchaseId);
            return Success("更新成功！");
        }
        #endregion

    }
}
