using Learun.Application.Extention.PortalSiteManage;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_PortalSite.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-01-2 09:35
    /// 描 述：首页设置
    /// </summary>
    public class HomeConfigController : MvcControllerBase
    {
        private HomeConfigIBLL homeConfigIBLL = new HomeConfigBLL();

        #region 视图功能
        /// <summary>
        /// 首页配置页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 设置文字
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetTextForm()
        {
            return View();
        }

        /// <summary>
        /// 顶部菜单设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TopMenuIndex()
        {
            return View();
        }
        /// <summary>
        /// 顶部菜单设置（表单）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TopMenuForm()
        {
            return View();
        }


        /// <summary>
        /// 底部菜单设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BottomMenuIndex()
        {
            return View();
        }
        /// <summary>
        /// 底部菜单设置(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BottomMenuForm()
        {
            return View();
        }

        /// <summary>
        /// 配置轮播图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PictureForm()
        {
            return View();
        }

        /// <summary>
        /// 添加模块（选择类型）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult  SelectModuleForm()
        {
            return View();
        }


        /// <summary>
        /// 模块1配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleForm1() {
            return View();
        }
        /// <summary>
        /// 模块2配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleForm2()
        {
            return View();
        }
        /// <summary>
        /// 模块3配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleForm3()
        {
            return View();
        }

        /// <summary>
        /// 模块3添加tab标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddTabForm()
        {
            return View();
        }

        /// <summary>
        /// 模块4配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleForm4()
        {
            return View();
        }
        /// <summary>
        /// 模块5配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleForm5()
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
        /// 获取树形数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetTree()
        {
            var data = homeConfigIBLL.GetTree();
            return Success(data);
        }

        /// <summary>
        /// 获取实体数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetEntity(string keyValue)
        {
            var data = homeConfigIBLL.GetEntity(keyValue);
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
            homeConfigIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(string keyValue, HomeConfigEntity entity)
        {
            homeConfigIBLL.SaveEntity(keyValue, entity);
            return SuccessString(entity.F_Id);
        }


        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateForm(string keyValue1,string keyValue2)
        {
            HomeConfigEntity entity1 = homeConfigIBLL.GetEntity(keyValue1);
            HomeConfigEntity entity2 = homeConfigIBLL.GetEntity(keyValue2);

            if (entity1 != null && entity2 != null) {
                int sort = (int)entity1.F_Sort;
                entity1.F_Sort = entity2.F_Sort;
                entity2.F_Sort = sort;

                homeConfigIBLL.SaveEntity(entity1.F_Id, entity1);
                homeConfigIBLL.SaveEntity(entity2.F_Id, entity2);
            } 

            return Success("更新成功！");
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveText(string type, string text)
        {
            homeConfigIBLL.SaveText(text, type);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string type)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                byte[] bytes = new byte[files[0].InputStream.Length];
                files[0].InputStream.Read(bytes, 0, bytes.Length);

                string strBase64 = Convert.ToBase64String(bytes);
                homeConfigIBLL.SaveImg(strBase64, files[0].FileName, FileEextension, type);
            }

            return Success("保存成功。");
        }
        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="sort">排序码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile2(string keyValue,int sort)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                byte[] bytes = new byte[files[0].InputStream.Length];
                files[0].InputStream.Read(bytes, 0, bytes.Length);

                string strBase64 = Convert.ToBase64String(bytes);
                homeConfigIBLL.SaveImg2(strBase64, files[0].FileName, FileEextension, keyValue, sort);
            }

            return Success("保存成功。");
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
        #endregion
    }
}