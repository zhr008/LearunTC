using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.ERPDemo.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 09:44
    /// 描 述：收款单
    /// </summary>
    public class ReceiptInfoController : MvcControllerBase
    {
        private ReceiptInfoIBLL receiptInfoIBLL = new ReceiptInfoBLL();

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
            var data = receiptInfoIBLL.GetPageList(paginationobj, queryJson);
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
            var LR_ERP_ReceiptInfoData = receiptInfoIBLL.GetLR_ERP_ReceiptInfoEntity( keyValue );
            var LR_ERP_ReceiptInfoDetailData = receiptInfoIBLL.GetLR_ERP_ReceiptInfoDetailList( LR_ERP_ReceiptInfoData.F_Id );
            var jsonData = new {
                LR_ERP_ReceiptInfo = LR_ERP_ReceiptInfoData,
                LR_ERP_ReceiptInfoDetail = LR_ERP_ReceiptInfoDetailData,
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
            receiptInfoIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string strEntity, string strlR_ERP_ReceiptInfoDetailList)
        {
            LR_ERP_ReceiptInfoEntity entity = strEntity.ToObject<LR_ERP_ReceiptInfoEntity>();
            List<LR_ERP_ReceiptInfoDetailEntity> lR_ERP_ReceiptInfoDetailList = strlR_ERP_ReceiptInfoDetailList.ToObject<List<LR_ERP_ReceiptInfoDetailEntity>>();
            receiptInfoIBLL.SaveEntity(keyValue,entity,lR_ERP_ReceiptInfoDetailList);
            if (string.IsNullOrEmpty(keyValue))
            {
            }
            return Success("保存成功！");
        }
        [HttpGet]
        
        public ActionResult GetPurchasePageList(string pagination, string purchaseId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = receiptInfoIBLL.GetPurchasePageList(paginationobj, purchaseId);
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
            receiptInfoIBLL.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
            receiptInfoIBLL.UpdatePurchase(purchaseId);
            return Success("更新成功！");
        }
        #endregion

    }
}
