using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;
using DataVisualization.core;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 19:59
    /// 描 述：培训记录
    /// </summary>
    public class PersonnelTrainController : MvcControllerBase
    {
        private PersonnelTrainIBLL personnelTrainIBLL = new PersonnelTrainBLL();
        private PersonnelsIBLL personnelsIBLL = new PersonnelsBLL();
        private CredentialsIBLL credentialsIBLL = new CredentialsBLL();
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
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = personnelTrainIBLL.GetPageList(paginationobj, queryJson);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFormData(string keyValue)
        {
            var tc_PersonnelTrainData = personnelTrainIBLL.Gettc_PersonnelTrainEntity(keyValue);
            var jsonData = new
            {
                tc_PersonnelTrain = tc_PersonnelTrainData,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteForm(string keyValue)
        {
            personnelTrainIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            tc_PersonnelTrainEntity entity = strEntity.ToObject<tc_PersonnelTrainEntity>();
            personnelTrainIBLL.SaveEntity(keyValue, entity);
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.F_TrainStatus == 6)
                {
                    return Fail("不能直接培训完成，需要同步人才库！");
                }
            }
            else 
            {
                if (entity.F_TrainStatus == 6)
                {
                    return Fail("培训完成，不允许修改！");
                }
            }
            return Success("保存成功！");
        }
        #endregion



        [HttpPost]
        public ActionResult SyncToCredentials(string keyValue)
        {
            var entity = personnelTrainIBLL.Gettc_PersonnelTrainEntity(keyValue);
            if (entity.F_TrainStatus != 5)
            {
                return Fail("未取得证书,不能同步到人才证书库");
            }
            var certEntity = credentialsIBLL.Gettc_CredentialsEntity(keyValue, entity.F_CertType, entity.F_MajorType);
            if (certEntity != null)
            {
                return Fail("已经同步到人才证书库，无需再次同步！");
            }
            tc_CredentialsEntity info = new tc_CredentialsEntity
            {
                F_UserName = entity.F_UserName,
                F_IDCardNo = entity.F_UserName,
                F_PersonId = entity.F_PersonId,
                F_CertType = entity.F_CertType,
                F_MajorType = entity.F_MajorType,
                F_Major = entity.F_Major,
                F_CertOrganization = entity.F_CertOrganization,
                F_CertDateBegin = entity.F_CertDateBegin,
                F_CertDateEnd = entity.F_CertDateEnd,
                F_CertStatus = entity.F_CertStatus,
                F_CertStyle = entity.F_CertStyle,
                F_PracticeStyle = 1,   //默认无
                F_PracticeSealStyle = 1,  //默认无
                F_CheckInTime = System.DateTime.Now
            };
            credentialsIBLL.SaveEntity("", info);
            entity.F_TrainStatus = 6;
            personnelTrainIBLL.SaveEntity(entity.F_PersonnelTrainId, entity);
            return Success("同步成功！");
        }
    }
}
