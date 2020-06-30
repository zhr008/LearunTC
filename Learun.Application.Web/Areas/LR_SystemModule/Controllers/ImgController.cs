using Learun.Application.Base.SystemModule;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    public class ImgController : MvcControllerBase
    {
        private ImgIBLL imgIBLL = new ImgBLL();
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string keyValue)
        {
            imgIBLL.GetImg(keyValue);
            return Success("获取成功！");
        }
    }
}