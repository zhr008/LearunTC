using Learun.Application.Base.SystemModule;
using Learun.Application.Extention.PortalSiteManage;
using Learun.Util;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_PortalSite.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2019-01-2 09:35
    /// 描 述：子页面管理
    /// </summary>
    public class PageController : MvcControllerBase
    {
        private PageIBLL pageIBLL = new PageBLL();
        private ImgIBLL imgIBLL = new ImgBLL();
        #region 视图功能
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 选择类型页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectForm()
        {
            return View();
        }
        /// <summary>
        /// 设置名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetNameForm() {
            return View();
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetContentForm()
        {
            return View();
        }

        /// <summary>
        /// 设置分类项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetCategoryForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pageIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetList()
        {
            var data = pageIBLL.GetList();
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
            var data = pageIBLL.GetEntity(keyValue);
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
            var entity = pageIBLL.GetEntity(keyValue);
            pageIBLL.DeleteEntity(keyValue);

            if (!string.IsNullOrEmpty(entity.F_Img))
            {
                imgIBLL.DeleteEntity(entity.F_Img);
            }

            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly, ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, PageEntity entity)
        {
            pageIBLL.SaveEntity(keyValue, entity);


            return Success("保存成功！");
        }



        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UploadFile(string keyValue, PageEntity entity)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                pageIBLL.SaveEntity(keyValue, entity);
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                ImgEntity imgEntity = null;
                if (string.IsNullOrEmpty(entity.F_Img))
                {
                    imgEntity = new ImgEntity();
                }
                else
                {
                    imgEntity = imgIBLL.GetEntity(entity.F_Img);
                }
                imgEntity.F_Name = files[0].FileName;
                imgEntity.F_ExName = FileEextension;

                byte[] bytes = new byte[files[0].InputStream.Length];
                files[0].InputStream.Read(bytes, 0, bytes.Length);

                imgEntity.F_Content = Convert.ToBase64String(bytes);

                imgIBLL.SaveEntity(entity.F_Img, imgEntity);

                entity.F_Img = imgEntity.F_Id;
                pageIBLL.SaveEntity(keyValue, entity);
            }

            return Success("保存成功。");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string keyValue)
        {
            pageIBLL.GetImg(keyValue);
            return Success("获取成功。");
        }
        #endregion
    }
}