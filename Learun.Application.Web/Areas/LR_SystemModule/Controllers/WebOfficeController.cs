using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    [HandlerLogin(FilterMode.Ignore)]
    public class WebOfficeController : MvcControllerBase
    {
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();
        // GET: LR_SystemModule/WebOffice
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        public void DownAnnexesFile(string fileId)
        {
            var data = annexesFileIBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath;
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        public void DownAnnexesFileNew(string fileId)
        {
            var data = annexesFileIBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath;
            var redPrintPath = data.F_FilePath.Replace(data.F_FileExtensions, "") + "_s" + data.F_FileExtensions;
            if (FileDownHelper.FileExists(redPrintPath))
            {
                FileDownHelper.DownLoadold(redPrintPath, filename);
            }
            else
            {
                if (FileDownHelper.FileExists(filepath))
                {
                    FileDownHelper.DownLoadold(filepath, filename);
                }
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        public ActionResult DownAnnexesFileByFolder(string folderid)
        {
            var list = annexesFileIBLL.GetList(folderid).ToList();
            if (list.Count > 0)
            {
                var data = list[0];
                string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
                string filepath = data.F_FilePath;
                var mime = MimeMapping.GetMimeMapping(filename);
                return File(new FileStream(filepath, FileMode.Open), mime, filename);
            }
            return null;
        }
        public void UploadAnnexesFileByFolder(string folderid)
        {
            var list = annexesFileIBLL.GetList(folderid).ToList();
            if (list.Count > 0)
            {
                var data = list[0];
                string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
                string filepath = data.F_FilePath;
                if (Request.Files.Count > 0)
                {
                    Request.Files[0].SaveAs(filepath);
                }
                //byte[] b = new byte[Convert.ToInt32(Request.InputStream.Length)];
                //Request.InputStream.Read(b, 0, Convert.ToInt32(Request.InputStream.Length));
                //FileStream fs = new FileStream(filepath, FileMode.Create);
                //BinaryWriter binWriter = new BinaryWriter(fs);
                //binWriter.Write(b, 0, Convert.ToInt32(Request.InputStream.Length));
                //binWriter.Close();
                //fs.Close();
            }
        }
        public void UploadAnnexesFile(string fileId)
        {
            var data = annexesFileIBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath;
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(filepath);
            }
            //byte[] b = new byte[Convert.ToInt32(Request.InputStream.Length)];
            //Request.InputStream.Read(b, 0, Convert.ToInt32(Request.InputStream.Length));
            //FileStream fs = new FileStream(filepath, FileMode.Create);
            //BinaryWriter binWriter = new BinaryWriter(fs);
            //binWriter.Write(b, 0, Convert.ToInt32(Request.InputStream.Length));
            //binWriter.Close();
            //fs.Close();
        }
        public void UploadAnnexesFileNew(string fileId)
        {
            var data = annexesFileIBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath.Replace(data.F_FileExtensions, "") + "_s" + data.F_FileExtensions;
            var f = Request.Files;
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(filepath);
            }
            //byte[] b = new byte[Convert.ToInt32(Request.InputStream.Length)];
            //Request.InputStream.Read(b, 0, Convert.ToInt32(Request.InputStream.Length));
            //FileStream fs = new FileStream(filepath, FileMode.Create);
            //BinaryWriter binWriter = new BinaryWriter(fs);
            //binWriter.Write(b, 0, Convert.ToInt32(Request.InputStream.Length));
            //binWriter.Close();
            //fs.Close();
        }
        //[HttpGet]
        //public ActionResult GetImg(string keyValue)
        //{
        //    lr_StampManageIBLL.GetImg(keyValue);
        //    return Success("获取成功！");
        //}

        //[HttpGet]
        //[AjaxOnly]
        //public ActionResult GetList(string keyword)
        //{
        //    var data = lr_StampManageIBLL.GetList(keyword);
        //    return Success(data);
        //}

        //[HttpPost]
        //[AjaxOnly]
        //public ActionResult EqualForm(string keyValue, string Password)
        //{
        //    var result = lr_StampManageIBLL.EqualPassword(keyValue, Password);
        //    if (result)
        //    {
        //        return Success("密码验证成功！");
        //    }
        //    else
        //    {
        //        return Fail("密码不正确！");
        //    }

        //}
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetFileIdByFolderId(string folderid)
        {
            var list = annexesFileIBLL.GetList(folderid).ToList();
            if (list.Count > 0)
            {
                return Json(new
                {
                    result = true,
                    fileid = list[0].F_Id
                });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        //public void PreviewFile(string fileid)
        //{
        //    var data = annexesFileIBLL.GetEntity(fileid);
        //    string filename = Server.UrlDecode(data.F_FileName);//客户端保存的文件名  
        //    string filepath = data.F_FilePath;//路径 
        //    FileConvertoPdfUtil.PreviewFile(data.F_FilePath, data.F_FileType);
        //    return;
        //    //if (data.F_FileType == "xlsx" || data.F_FileType == "xls")
        //    //{
        //    //    filepath = filepath.Substring(0, filepath.LastIndexOf(".")) + ".pdf";//文件名
        //    //    if (!DirFileHelper.IsExistFile(filepath))
        //    //    {
        //    //        filePreviewIBLL.GetExcelData(data.F_FilePath);
        //    //    }
        //    //}
        //    //if (data.F_FileType == "docx" || data.F_FileType == "doc")
        //    //{
        //    //    filepath = filepath.Substring(0, filepath.LastIndexOf(".")) + ".pdf";//文件名
        //    //    if (!DirFileHelper.IsExistFile(filepath))
        //    //    {
        //    //        filePreviewIBLL.GetWordDataByAspose(data.F_FilePath);
        //    //    }
        //    //}
        //    //Response.ClearContent();
        //    //switch (data.F_FileType)
        //    //{
        //    //    case "jpg":
        //    //        Response.ContentType = "image/jpeg";
        //    //        break;
        //    //    case "gif":
        //    //        Response.ContentType = "image/gif";
        //    //        break;
        //    //    case "png":
        //    //        Response.ContentType = "image/png";
        //    //        break;
        //    //    case "bmp":
        //    //        Response.ContentType = "application/x-bmp";
        //    //        break;
        //    //    case "jpeg":
        //    //        Response.ContentType = "image/jpeg";
        //    //        break;
        //    //    case "doc":
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //    case "docx":
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //    case "ppt":
        //    //        Response.ContentType = "application/x-ppt";
        //    //        break;
        //    //    case "pptx":
        //    //        Response.ContentType = "application/x-ppt";
        //    //        break;
        //    //    case "xls":
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //    case "xlsx":
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //    case "pdf":
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //    case "txt":
        //    //        Response.ContentType = "text/plain";
        //    //        break;
        //    //    case "csv":
        //    //        Response.ContentType = "";
        //    //        break;
        //    //    default:
        //    //        Response.ContentType = "application/pdf";
        //    //        break;
        //    //}
        //    //Response.Charset = "GB2312";
        //    //Response.WriteFile(filepath);
        //}
        //[HttpPost]
        //public ActionResult GetDocTemplateFileInfos(string keyValue)
        //{
        //    Func<string, string> func = folderid =>
        //    {
        //        return AnnexesFileIBLL.GetList(folderid).ToList().Count > 0 ? folderid : "";
        //    };
        //    var docModel = dC_OA_DispatchOfficialDocManagerIBLL.GetDC_OA_DispatchOfficialDocEntity(keyValue);
        //    if (!string.IsNullOrWhiteSpace(docModel.F_RedHead))
        //    {
        //        var tempModel = dC_OA_DispatchOfficialPaintTemplateIBLL.GetDC_OA_DispatchOfficialPaintTemplateEntity(docModel.F_RedHead);
        //        return Json(new
        //        {
        //            fileid = docModel.F_FileContent_New,
        //            template = new
        //            {
        //                first = func(tempModel.F_FirstTemplate),
        //                second = func(tempModel.F_SecoundTemplate),
        //                third = func(tempModel.F_ThirdTemplate)
        //            }
        //        });
        //    }
        //    return Json(new
        //    {
        //        fileid = docModel.F_FileContent
        //    });
        //}
    }
}