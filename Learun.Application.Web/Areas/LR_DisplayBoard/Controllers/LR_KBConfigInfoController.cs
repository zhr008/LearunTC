using Learun.Application.Extention.DisplayBoardManage;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoController : MvcControllerBase
    {
        private LR_KBConfigInfoIBLL lR_KBConfigInfoIBLL = new LR_KBConfigInfoBLL();

        #region 视图功能
        /// <summary>
        /// 表格配置页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TableForm()
        {
             return View();
        }
        /// <summary>
        /// 统计指标新增编辑界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StatisticsForm()
        {
            return View();
        }
        /// <summary>
        /// 统计指标单个配置弹出界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ColStatisForm()
        {
            return View();
        }
        /// <summary>
        /// 饼图/柱状图/折线/仪表盘 模块配置界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChartForm()
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
            var data = lR_KBConfigInfoIBLL.GetList(queryJson);
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
            var data = lR_KBConfigInfoIBLL.GetPageList(paginationobj, queryJson);
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
            var data = lR_KBConfigInfoIBLL.GetEntity(keyValue);
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
            lR_KBConfigInfoIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue,LR_KBConfigInfoEntity entity)
        {
            lR_KBConfigInfoIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 根据配置信息获取数据
        /// </summary>
        /// <param name="configuration">配置信息</param>
        /// <param name="type">类型statistics统计;2表格3图表</param>
        /// <returns></returns>
        
        public ActionResult GetConfigData(string configInfoList)
        {
            List<ConfigInfoModel> list = configInfoList.ToObject<List<ConfigInfoModel>>();

            var data = lR_KBConfigInfoIBLL.GetConfigData(list);
            return Success(data);
        }
        /// <summary>
        /// 根据路径获取接口数据（仅限get方法）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <returns></returns>
        public ActionResult GetApiData(string path)
        {
            var data = lR_KBConfigInfoIBLL.GetApiData(path);
            return Success(data);
        }
        #endregion
    }
}
