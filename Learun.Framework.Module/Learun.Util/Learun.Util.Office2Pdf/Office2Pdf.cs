using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace Learun.Util.Office2Pdf
{
    public class Office2Pdf
    {
        #region office文件转换为pdf文件

        /// <summary>
        /// 将word文档转换成PDF格式
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param> 
        /// <param name="exportFormat"></param>
        /// <returns></returns>
        public bool WordConvertPDF(string sourcePath, string targetPath = null)
        {
            if (sourcePath == null) { throw new ArgumentNullException("wpsFilename"); }
            //保存路径为空时，保存在原始文件目录
            if (targetPath == null)
            {
                targetPath = Path.ChangeExtension(sourcePath, "pdf");
            }
            bool result;
            object paramMissing = Type.Missing;
            Microsoft.Office.Interop.Word.ApplicationClass wordApplication = new Microsoft.Office.Interop.Word.ApplicationClass();
            Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Word.WdExportFormat paramExportFormat = Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                        Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                        Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 将excel文档转换成PDF格式
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param> 
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool ExcelConvertPDF(string sourcePath, string targetPath = null)
        {
            if (sourcePath == null) { throw new ArgumentNullException("wpsFilename"); }
            //保存路径为空时，保存在原始文件目录
            if (targetPath == null)
            {
                targetPath = Path.ChangeExtension(sourcePath, "pdf");
            }
            bool result;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.ApplicationClass application = null;
            Workbook workBook = null;
            try
            {
                application = new Microsoft.Office.Interop.Excel.ApplicationClass();
                object target = targetPath;
                object type = XlFixedFormatType.xlTypePDF;
                workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                        missing, missing, missing, missing, missing, missing, missing, missing, missing);

                workBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, target, XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 将ppt文档转换成pdf格式
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param> 
        /// <param name="targetFileType"></param>
        /// <returns></returns>
        public bool PPTConvertPDF(string sourcePath, string targetPath = null)
        {
            if (sourcePath == null) { throw new ArgumentNullException("wpsFilename"); }
            //保存路径为空时，保存在原始文件目录
            if (targetPath == null)
            {
                targetPath = Path.ChangeExtension(sourcePath, "pdf");
            }
            bool result;
            object missing = Type.Missing;
            Microsoft.Office.Interop.PowerPoint.ApplicationClass application = null;
            Presentation persentation = null;
            try
            {
                
                   application = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
                //persentation = application.Presentations.Open(sourcePath, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                //persentation.SaveAs(targetPath, PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoTrue);

                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        #endregion
    }
}
