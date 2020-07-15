using Learun.Util;
using System.Data;
using Learun.Application.Report;
using System.Web.Mvc;
using System.Collections.Generic;
using Learun.Application.Base.SystemModule;
using Learun.Application.CRM;
using Learun.Application.TwoDevelopment.LR_CodeDemo;

namespace Learun.Application.Web.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 15:17
    /// 描 述：报表文件管理
    /// </summary>
    public class ReportShowController : MvcControllerBase
    {
        private RptRelationIBLL rptRelationIBLL = new RptRelationBLL();
        private CrmChanceService crmChanceService = new CrmChanceService();


        private PurchaseOrderIBLL purchaseOrderIBLL = new PurchaseOrderBLL();
        private PurchaseRequisitionIBLL purchaseRequisitionIBLL = new PurchaseRequisitionBLL();
        private PurchaseWarehousingIBLL purchaseWarehousingIBLL = new PurchaseWarehousingBLL();
        private PaymentInfoIBLL paymentInfoIBLLIBLL = new PaymentInfoBLL();
        private ReceiptInfoIBLL receiptInfoIBLL = new ReceiptInfoBLL();
        private SalesOfferIBLL salesOfferIBLL = new SalesOfferBLL();
        private SalesOrderIBLL salesOrderIBLL = new SalesOrderBLL();
        private SalesReceiptIBLL salesReceiptIBLL = new SalesReceiptBLL();

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
        #endregion

        #region 获取数据

        [HttpGet]
        
        public ActionResult GetData()
        {
            var data = crmChanceService.GetList();
            return Success(data);
        }

        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetTableField()
        {
            var data = purchaseOrderIBLL.GetTableField();
            return Success(data);
        }

        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetTableDate(string param)
        {
            var data = new object();

            switch(param)
            {
                case "LR_ERP_PaymentInfo":
                    data = paymentInfoIBLLIBLL.GetTableDate();
                    break;
                case "LR_ERP_ReceiptInfo":
                    data = receiptInfoIBLL.GetTableDate();
                    break;
                case "LR_ERP_SalesReceipt":
                    data = salesReceiptIBLL.GetTableDate();
                    break;
                case "LR_ERP_SalesOrder":
                    data = salesOrderIBLL.GetTableDate();
                    break;
                case "LR_ERP_SalesOffer":
                    data = salesOfferIBLL.GetTableDate();
                    break;
                case "LR_ERP_PurchaseWarehousing":
                    data = purchaseWarehousingIBLL.GetTableDate();
                    break;
                case "LR_ERP_PurchaseRequisition":
                    data = purchaseRequisitionIBLL.GetTableDate();
                    break;
                case "LR_ERP_PurchaseOrder":
                    data = purchaseOrderIBLL.GetTableDate();
                    break;
                default:
                    data = purchaseOrderIBLL.GetTableDate();
                    break;

            }
         
            return Success(data);
        }
        
        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = rptRelationIBLL.GetPageList(paginationobj, queryJson);
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
            var LR_RptRelationData = rptRelationIBLL.GetLR_RptRelationEntity(keyValue);
            var jsonData = new
            {
                LR_RptRelation = LR_RptRelationData,
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
            rptRelationIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            LR_RPT_RelationEntity entity = strEntity.ToObject<LR_RPT_RelationEntity>();
            rptRelationIBLL.SaveEntity(keyValue, entity);
            if (string.IsNullOrEmpty(keyValue))
            {
            }
            return Success("保存成功！");
        }
        #endregion
    }
}
