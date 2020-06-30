using Learun.Application.Extention.TaskScheduling;
using Learun.Application.TwoDevelopment.LR_CodeDemo.QueueDemo;
using Learun.Application.WorkFlow;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Learun.Application.Web
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：应用程序全局设置
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 启动的时候清除全部缓存
            ICache cache = CacheFactory.CaChe();
            cache.RemoveAll(6);

            WfJobScheduler.Start();
            QuartzHelper.InitJob();
            #region 队列处理示例
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //    while (true)
            //    {
            //        if (cache.ListLength("TicketQueue") > 50)
            //        {
            //            cache.Remove("TicketQueue");
            //        }
            //        if (cache.ListLength("BuyQueue") > 0)
            //        {
            //            Random rd = new Random();
            //            int carNo = rd.Next(10, 18);
            //            string[] seat = { "A", "B", "C", "D", "E", "F" };
            //            Buyer buyer = cache.ListRightPop<Buyer>("BuyQueue");
            //            //写入车票队列
            //            cache.ListLeftPush("TicketQueue", new { name = buyer.name, id = buyer.id, ticketdate = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm"), code = carNo + "车" + (carNo / 2).ToString() + seat[rd.Next(0, 6)] });
            //            Thread.Sleep(1000);//假装处理这个业务花了1毫秒
            //        }
            //        else
            //        {
            //            Thread.Sleep(3000);//防止CPU空转
            //        }

            //        if (cache.ListLength("BuyQueue") > 10)
            //        {
            //            cache.Remove("BuyQueue");
            //        }
            //    }
            //});

        }
        #endregion

        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
        }
    }
}
