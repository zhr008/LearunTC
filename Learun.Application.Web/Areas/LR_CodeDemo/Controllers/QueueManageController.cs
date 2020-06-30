using Learun.Application.TwoDevelopment.LR_CodeDemo.QueueDemo;
using Learun.Application.TwoDevelopment.SystemDemo;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架
    /// Copyright (c) 2013-2019 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2019.04.12
    /// 描 述：消息队列
    /// </summary>
    public class QueueManageController : MvcControllerBase
    {
        ICache cache = CacheFactory.CaChe();
        /// <summary>
        /// 普通表格
        /// </summary>
        /// <returns></returns>
        public ActionResult QueueDemo()
        {
            return View();
        }


        /// <summary>
        /// 获取购票人列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBuyerList()
        {
            var data = cache.ListRange<Buyer>("BuyQueue");
            return Success(data);
        }
        /// <summary>
        /// 获取车票列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTicketList()
        {
            var data = cache.ListRange<Ticket>("TicketQueue");
            return Success(data);
        }
        /// <summary>
        /// 保存表格
        /// </summary>
        /// <param name="jsondata">表格数据 </param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveList(string list)
        {
            if (!string.IsNullOrEmpty(list))
            {
                var jsonlist = list.ToJObject();


                foreach (var item in jsonlist)
                {
                    Random rd = new Random();
                    rd.Next(1965, 2005);
                    string id = "340283";
                    id = id + rd.Next(1965, 2005).ToString();//身份证年
                    id = id + "0" + rd.Next(1, 9).ToString();//身份证月
                    id = id + rd.Next(10, 28).ToString();//身份证日
                    id = id + rd.Next(3515, 4419).ToString();//
                    cache.ListLeftPush("BuyQueue", new { name = item.Value, id = id });
                }
                //List<CrmChanceEntity> list = cache.ListRange<CrmChanceEntity>("chanceQueue");
                //CrmChanceEntity item = cache.ListRightPop<CrmChanceEntity>("chanceQueue");
            }
            return Success("操作成功。30秒内请勿重复提交");
        }
    }
}