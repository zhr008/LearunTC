using Excel;
using PowerPoint;
using System;
using System.IO;
using Word;
namespace Learun.Util.Wps
{
    /// <summary>
    /// 通过WPS将office转PDF
    /// </summary>
    public class Wps2Pdf : IDisposable
    {
        dynamic wps;
        /// <summary>
        /// Word转PDF
        /// </summary>
        /// <param name="wpsFilename">文件名</param>
        /// <param name="pdfFilename"></param>
        public void WordToPdf(string wpsFilename, string pdfFilename = null)
        {
            Type type = Type.GetTypeFromProgID("KWps.Application");
            wps = Activator.CreateInstance(type);
            if (wpsFilename == null) { throw new ArgumentNullException("wpsFilename"); }

            if (pdfFilename == null)
            {
                pdfFilename = Path.ChangeExtension(wpsFilename, "pdf");
            }
            dynamic doc = wps.Documents.Open(wpsFilename, Visible: false);

            doc.ExportAsFixedFormat(pdfFilename, WdExportFormat.wdExportFormatPDF);
            doc.Close();
            Dispose();
        }
        /// <summary>
        /// Excel转PDF
        /// </summary>
        /// <param name="wpsFilename">文件名</param>
        /// <param name="pdfFilename"></param>
        public void ExcelToPdf(string wpsFilename, string pdfFilename = null)
        {
            Excel.Application eps = new Excel.Application();
            try
            {
                if (wpsFilename == null) { throw new ArgumentNullException("wpsFilename"); }
                //保存路径为空时，保存在原始文件目录
                if (pdfFilename == null)
                {
                    pdfFilename = Path.ChangeExtension(wpsFilename, "pdf");
                }
                //忽略警告提示
                eps.Visible = false;
                eps.DisplayAlerts = false;
                eps.Workbooks.Open(wpsFilename);
                eps.ActiveWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFilename,null,null,true);
                eps.ActiveWorkbook.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //无论是否成功，都退出
                if (eps != null)
                {
                    eps.Quit();
                    //强制关闭所有et进程
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                    foreach (System.Diagnostics.Process prtemp in process)
                    {
                        prtemp.Kill();
                    }
                }
            }



        }
        /// <summary>
        /// PPT转PDF
        /// </summary>
        /// <param name="wpsFilename">文件名</param>
        /// <param name="pdfFilename"></param>
        public void PptToPdf(string wpsFilename, string pdfFilename = null)
        {

            PowerPoint.Application pps = new PowerPoint.Application();

            try
            {
                if (wpsFilename == null) { throw new ArgumentNullException("wpsFilename"); }
                //保存路径为空时，保存在原始文件目录
                if (pdfFilename == null)
                {
                    pdfFilename = Path.ChangeExtension(wpsFilename, "pdf");
                }
                //忽略警告提示 此处无法设置，原因不清楚！
                //pps.DisplayAlerts = WPP.WpAlertLevel.wpAlertsAll;
                //应用程序不可见
               // pps.
               // pps.Visible=MsoTriState.
                pps.Visible =MsoTriState.msoTrue;
                //pps.DisplayAlerts = PpAlertLevel.ppAlertsNone;
                pps.Presentations.Open(wpsFilename);
                pps.ActivePresentation.ExportAsFixedFormat(pdfFilename, PpFixedFormatType.ppFixedFormatTypePDF);
                pps.ActivePresentation.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //无论是否成功，都退出
                if (pps != null)
                {
                    pps.Quit();
                    //强制关闭所有wpp进程
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("POWERPNT");
                    foreach (System.Diagnostics.Process prtemp in process)
                    {
                        prtemp.Kill();
                    }
                }
            }
        }

        public void Dispose()
        {
            if (wps != null) { wps.Quit(); }
        }
    }
}
