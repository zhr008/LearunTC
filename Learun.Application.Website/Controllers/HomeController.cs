using Learun.Application.Extention.PortalSiteManage;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Website.Controllers
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.25
    /// 描 述：主页控制器
    /// </summary>
    public class HomeController : MvcControllerBase
    {
        private HomeConfigIBLL homeConfigIBLL = new HomeConfigBLL();
        private ArticleIBLL articleIBLL = new ArticleBLL();
        private PageIBLL pageIBLL = new PageBLL();

        #region 视图功能
        /// <summary>
        /// 首页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 子页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChildIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListIndex()
        {
            return View();
        }
        /// <summary>
        /// 图表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImgListIndex()
        {
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取全部数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllList()
        {
            var data = homeConfigIBLL.GetALLList();
            return Success(data);
        }
        /// <summary>
        /// 获取数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetList(string type)
        {
            var data = homeConfigIBLL.GetList(type);
            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageData(string keyValue)
        {
            var data = pageIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 扩展功能
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string type)
        {
            homeConfigIBLL.GetImg(type);
            return Success("获取成功。");
        }

        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg2(string keyValue)
        {
            homeConfigIBLL.GetImg2(keyValue);
            return Success("获取成功。");
        }
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticleImg(string keyValue)
        {
            articleIBLL.GetImg(keyValue);
            return Success("获取成功。");
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticle(string keyValue)
        {
            var data = articleIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticlePageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = articleIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageImg(string keyValue)
        {
            pageIBLL.GetImg2(keyValue);
            return Success("获取成功。");
        }
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUeditorImg(string id)
        {
            string path = Config.GetValue("imgPath")+ "/ueditor/upload/image"+ id;
            path = System.Text.RegularExpressions.Regex.Replace(path, @"\s", "");
            if (FileDownHelper.FileExists(path))
            {
                FileDownHelper.DownLoadold(path, id.Split('/')[2]);
            }
            return Success("获取成功");
        }
        #endregion
    }
}