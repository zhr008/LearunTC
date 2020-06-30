using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public class GantProjectController : MvcControllerBase
    {
        private GantProjectIBLL gantProjectIBLL = new GantProjectBLL();

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
        /// 甘特图
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Gant()
        {
            return View();
        }
        /// <summary>
        /// 项目主表
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Project()
        {
            return View();
        }
        /// <summary>
        /// 项目明细
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProjectDetail()
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
            var data = gantProjectIBLL.GetPageList(paginationobj, queryJson);
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
            var LR_OA_ProjectData = gantProjectIBLL.GetLR_OA_ProjectEntity(keyValue);
            var LR_OA_ProjectDetailData = gantProjectIBLL.GetLR_OA_ProjectDetailList(LR_OA_ProjectData.F_Id);
            var jsonData = new
            {
                LR_OA_Project = LR_OA_ProjectData,
                LR_OA_ProjectDetail = LR_OA_ProjectDetailData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取甘特图明细数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProjectList(string keyword)
        {
            List<object> list = new List<object>();
            var projectList = gantProjectIBLL.GetList(keyword);
            foreach (var item in projectList)
            {
                List<object> timeList = new List<object>();
                timeList.Add(new
                {
                    beginTime = item.F_StartTime.ToString(),
                    endTime = item.F_EndTime.ToString(),
                    color = string.IsNullOrEmpty(item.F_Status) ? "#3286ed" : item.F_Status,
                    overtime = false,
                    text = item.F_ProjectName
                });
                var data = new
                {
                    id = item.F_Id,
                    text = item.F_ProjectName,
                    isexpand = false,
                    complete = false,
                    timeList = timeList,
                    hasChildren = true
                };
                if (!string.IsNullOrEmpty(keyword))
                {
                    if (data.text.IndexOf(keyword) != -1)
                    {
                        list.Add(data);
                    }
                }
                else
                {
                    list.Add(data);
                };
            }
            return Success(list);
        }
        /// <summary>
        /// 获取甘特图数据
        /// </summary>
        /// <param name="parenId">父键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProjectDetail(string parentId)
        {
            List<object> list = new List<object>();
            var projectDetail = gantProjectIBLL.GetDetailList(parentId);
            foreach (var item in projectDetail)
            {
                List<object> timeList = new List<object>();
                timeList.Add(new
                {
                    beginTime = item.F_StartTime.ToString(),
                    endTime = item.F_EndTime.ToString(),
                    color = string.IsNullOrEmpty(item.F_Status) ? "#1bb99a" : item.F_Status,
                    overtime = false,
                    text = item.F_ItemName
                });


                var data = new
                {
                    id = item.F_Id,
                    text = item.F_ItemName,
                    isexpand = false,
                    complete = true,
                    timeList = timeList,
                    hasChildren = false
                };
                list.Add(data);
            }
            return Success(list);
        }
        /// <summary> 
        /// 获取表单数据 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGantData(string keyValue)
        {
            var LR_OA_ProjectData = gantProjectIBLL.GetLR_OA_ProjectEntity(keyValue);
            var jsonData = new
            {
                LR_OA_Project = LR_OA_ProjectData,
            };
            return Success(jsonData);
        }
        /// <summary> 
        /// 获取表单数据 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGantDetail(string keyValue)
        {
            var LR_OA_ProjectDetailData = gantProjectIBLL.GetLR_OA_ProjectDetailEntity(keyValue);
            var jsonData = new
            {
                LR_OA_ProjectDetail = LR_OA_ProjectDetailData,
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
            gantProjectIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 删除明细数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteDetail(string keyValue)
        {
            gantProjectIBLL.DeleteDetail(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strlR_OA_ProjectDetailList)
        {
            LR_OA_ProjectEntity entity = strEntity.ToObject<LR_OA_ProjectEntity>();
            List<LR_OA_ProjectDetailEntity> lR_OA_ProjectDetailList = strlR_OA_ProjectDetailList.ToObject<List<LR_OA_ProjectDetailEntity>>();
            gantProjectIBLL.SaveEntity(keyValue, entity, lR_OA_ProjectDetailList);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存表头实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveGant(string keyValue, string strEntity)
        {
            LR_OA_ProjectEntity entity = strEntity.ToObject<LR_OA_ProjectEntity>();
            gantProjectIBLL.SaveGant(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存明细实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveDetail(string keyValue, string strEntity)
        {
            LR_OA_ProjectDetailEntity entity = strEntity.ToObject<LR_OA_ProjectDetailEntity>();
            gantProjectIBLL.SaveDetail(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
