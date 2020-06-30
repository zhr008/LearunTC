using Learun.Application.Base.SystemModule;
using Learun.Application.OA.LR_StampManage;
using Learun.Util;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组（王飞）
    /// 日 期：2018.11.19
    /// 描 述：印章管理
    /// </summary>
    public class StampInfoController : MvcControllerBase
    {
        private LR_StampManageIBLL lr_StampManageIBLL = new LR_StampManageBLL();
        private ImgIBLL imgIBLL = new ImgBLL();

        #region 视图功能 
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            return View();
        }

        public ActionResult StampDetailIndex()
        {
            return View();
        }

        public ActionResult EqualForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = lr_StampManageIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取所有的印章信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string keyword)
        {
            var data = lr_StampManageIBLL.GetList(keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string keyValue)
        {
            lr_StampManageIBLL.GetImg(keyValue);
            return Success("获取成功！");
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存印章
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, LR_StampManageEntity entity)
        {
            lr_StampManageIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功。");

        }
        /// <summary>
        /// 删除印章
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            lr_StampManageIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">印章实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string keyValue, LR_StampManageEntity entity)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                lr_StampManageIBLL.SaveEntity(keyValue, entity);
            }
            else {
                string FileEextension = Path.GetExtension(files[0].FileName);
                ImgEntity imgEntity = null;
                if (string.IsNullOrEmpty(entity.F_ImgFile))
                {
                    imgEntity = new ImgEntity();
                }
                else
                {
                    imgEntity = imgIBLL.GetEntity(entity.F_ImgFile);
                }

                imgEntity.F_Name = files[0].FileName;
                imgEntity.F_ExName = FileEextension;
                byte[] bytes = new byte[files[0].InputStream.Length];
                files[0].InputStream.Read(bytes, 0, bytes.Length);

                imgEntity.F_Content = Convert.ToBase64String(bytes);
                imgIBLL.SaveEntity(entity.F_ImgFile, imgEntity);

                entity.F_ImgFile = imgEntity.F_Id;
                lr_StampManageIBLL.SaveEntity(keyValue, entity);
            }
            return Success("保存成功。");
        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpDateSate(string keyValue, int state)
        {
            lr_StampManageIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult EqualForm(string keyValue, string Password)
        {
            var result = lr_StampManageIBLL.EqualPassword(keyValue, Password);
            if (result)
            {
                return Success("密码验证成功！");
            }
            else
            {
                return Fail("密码不正确！");
            }

        }
        #endregion
    }
}