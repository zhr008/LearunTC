using Learun.Application.Base.Desktop;
using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Data;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_Desktop.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-05-28 16:08
    /// 描 述：桌面统计数据配置
    /// </summary>
    public class DTTargetController : MvcControllerBase
    {
        private DTTargetIBLL dTTargetIBLL = new DTTargetBLL();
        private DTListIBLL dTListIBLL = new DTListBLL();
        private DTChartIBLL dTChartIBLL = new DTChartBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();


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
        
        public ActionResult GetList()
        {
            var data = dTTargetIBLL.GetList();
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string queryJson)
        {
            var data = dTTargetIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFormData(string keyValue)
        {
            var data = dTTargetIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        ///  桌面统计图数据
        /// </summary>
        /// <param name="ver">版本号</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetMap(string ver)
        {
            var target = dTTargetIBLL.GetList();
            var list = dTListIBLL.GetList();
            var chart = dTChartIBLL.GetList();

            var data = new
            {
                target,
                list,
                chart
            };
            string md5 = Md5Helper.Encrypt(data.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = data,
                    ver = md5
                };
                return Success(jsondata);
            }
        }

        /// <summary>
        /// 获取统计指标数据
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetSqlData(string Id) {
            var data = dTTargetIBLL.GetEntity(Id);
            if (data != null && !string.IsNullOrEmpty(data.F_Sql))
            {
                DataTable dt = databaseLinkIBLL.FindTable(data.F_DataSourceId, data.F_Sql);
                var jsonData2 = new
                {
                    Id,
                    value = dt.Rows[0][0]
                };
                return Success(jsonData2);
            }
            else
            {
                var jsonData = new
                {
                    Id,
                    value = ""
                };
                return Success(jsonData);
            }
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
            dTTargetIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, DTTargetEntity entity)
        {
            dTTargetIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

    }


}