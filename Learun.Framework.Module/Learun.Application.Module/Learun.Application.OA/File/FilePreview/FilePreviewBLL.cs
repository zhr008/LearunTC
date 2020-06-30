using Spire.Xls;
using System;
//using Learun.Util;
using Spire.Doc;
//using System.IO;
//using Learun.Util.Office2Pdf;

namespace Learun.Application.OA.File.FilePreview
{
    public class FilePreviewBLL : FilePreviewIBLL
    {
        //Office2Pdf office2Pdf = new Office2Pdf();

        /// <summary>
        /// 获取EXCEL数据
        /// <summary>
        /// <returns></returns>
        public void GetExcelData(string path)
        {
            try
            {
                //office2Pdf.ExcelConvertPDF(path);//此处可在服务器安装office2007通过offic2007转换
                //load Excel file
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(path);
                workbook.SaveToFile(path.Substring(0, path.LastIndexOf(".")) + ".pdf", Spire.Xls.FileFormat.PDF);
            }
            catch (Exception)
            {
                throw (new Exception("文件丢失"));
            }
        }


        /// <summary>
        /// 获取Word数据
        /// <summary>
        /// <returns></returns>
        public void GetWordData(string path)
        {
            try
            {
                //office2Pdf.WordConvertPDF(path);此处可在服务器安装office2007通过offic2007转换
                Document document = new Document();
                document.LoadFromFile(path);
                document.SaveToFile(path.Substring(0, path.LastIndexOf(".")) + ".pdf", Spire.Doc.FileFormat.PDF);
            }
            catch (Exception)
            {
                throw (new Exception("文件丢失"));
            }
        }
        /// <summary>
        /// 获取Word数据
        /// <summary>
        /// <returns></returns>
        public void GetPptData(string path)
        {
            try
            {
                // office2Pdf.PPTConvertPDF(path);office2Pdf.WordConvertPDF(path);此处可在服务器安装office2007通过offic2007转换
                Document document = new Document();
                document.LoadFromFile(path);
                document.SaveToFile(path.Substring(0, path.LastIndexOf(".")) + ".pdf", Spire.Doc.FileFormat.PDF);
            }
            catch (Exception)
            {
                throw (new Exception("文件丢失"));
            }
        }
    }
}