using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.ERPDemo.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 17:55
    /// 描 述：销售订单
    /// </summary>
    public class SalesOrderController : MvcControllerBase
    {
        private SalesOrderIBLL salesOrderIBLL = new SalesOrderBLL();

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
        [HttpGet]
        public ActionResult ReceiptForm()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HistoryForm()
        {
            return View();
        }
        public ActionResult Report()
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
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = salesOrderIBLL.GetPageList(paginationobj, queryJson);
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
        
        public ActionResult GetFormData(string keyValue)
        {
            var LR_ERP_SalesOrderData = salesOrderIBLL.GetLR_ERP_SalesOrderEntity( keyValue );
            var LR_ERP_SalesOrderDetailData = salesOrderIBLL.GetLR_ERP_SalesOrderDetailList( LR_ERP_SalesOrderData.F_Id );
            var jsonData = new {
                LR_ERP_SalesOrder = LR_ERP_SalesOrderData,
                LR_ERP_SalesOrderDetail = LR_ERP_SalesOrderDetailData,
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
        
        public ActionResult DeleteForm(string keyValue)
        {
            salesOrderIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string strEntity, string strlR_ERP_SalesOrderDetailList)
        {
            LR_ERP_SalesOrderEntity entity = strEntity.ToObject<LR_ERP_SalesOrderEntity>();
            List<LR_ERP_SalesOrderDetailEntity> lR_ERP_SalesOrderDetailList = strlR_ERP_SalesOrderDetailList.ToObject<List<LR_ERP_SalesOrderDetailEntity>>();
            salesOrderIBLL.SaveEntity(keyValue,entity,lR_ERP_SalesOrderDetailList);
            if (string.IsNullOrEmpty(keyValue))
            {
            }
            return Success("保存成功！");
        }
        [HttpGet]
        
        public ActionResult GetPurchasePageList(string pagination, string purchaseId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = salesOrderIBLL.GetPurchasePageList(paginationobj, purchaseId);
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
        
        public ActionResult Update(string purchaseInfoId, string purchaseId)
        {
            salesOrderIBLL.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
            salesOrderIBLL.UpdatePurchase(purchaseId);
            return Success("更新成功！");
        }
        #endregion

    }
}
