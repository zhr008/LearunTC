using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2018.7.30
    /// 描 述：logo图片设置
    /// </summary>
    public class LogoImgController : MvcControllerBase
    {
        private LogoImgIBLL logoImgIBLL = new LogoImgBLL();

        #region 视图功能
        /// <summary>
        /// PC端图片设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PCIndex()
        {
            return View();
        }
        /// <summary>
        /// 移动端图片设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string code)
        {
            logoImgIBLL.GetImg(code);
            return Success("获取成功。");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string code)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }

            string FileEextension = Path.GetExtension(files[0].FileName);
            string fileHeadImg = Config.GetValue("fileLogoImg");
            string fullFileName = string.Format("{0}/{1}{2}", fileHeadImg, code, FileEextension);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            LogoImgEntity entity = new LogoImgEntity();
            entity.F_Code = code;
            entity.F_FileName = FileEextension;
            logoImgIBLL.SaveEntity(entity);
            return Success("上传成功。");
        }
        #endregion
    }
}