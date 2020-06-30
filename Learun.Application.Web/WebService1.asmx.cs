using GrapeCity.ActiveReports;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GrapeCity.ActiveReports.Document;
using Learun.Application.TwoDevelopment.LR_CodeDemo;

namespace Learun.Application.Web
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : GrapeCity.ActiveReports.Web.ReportService
    {
        SectionReport definition;
        PageReport definition1;
        PageDocument _pageDocument;
        string id;
        [WebMethod]
        protected override object OnCreateReportHandler(string reportPath)
        {
            string[] report = reportPath.Split('|');
            if (report.Length > 1)
            {
                reportPath = report[0];
                id = report[1];
            }
            definition1 = (PageReport)base.OnCreateReportHandler(reportPath);
            _pageDocument = new PageDocument(definition1);
            if (reportPath == "Reports/制程工单.rdlx")
            {
                definition1.Document.LocateDataSource += new LocateDataSourceEventHandler(DataSource1);
            }
            return definition1;
        }
        private void DataSource1(object sender, LocateDataSourceEventArgs args)
        {

            if (args.DataSet.Query.DataSourceName == "DataSource1")
            {
                if (args.DataSet.Name == "DataSet1")
                {
                    WorkOrderIBLL workOrderIBLL = new WorkOrderBLL();
                    args.Data = workOrderIBLL.GetPrintItem(id);
                }
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
