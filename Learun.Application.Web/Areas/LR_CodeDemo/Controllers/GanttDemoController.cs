using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.011.23
    /// 描 述：甘特图演示
    /// </summary>
    public class GanttDemoController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 显示静态本地数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index1()
        {
            return View();
        }
        /// <summary>
        /// 显示树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index2()
        {
            return View();
        }
        /// <summary>
        /// 动态加载后台数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index3()
        {
            return View();
        }
        /// <summary>
        /// 分页显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index4()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="parentId">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTimeList(string keyword, string parentId)
        {
            List<object> list = new List<object>();
            if (string.IsNullOrEmpty(parentId) || parentId == "0")
            {
                // 根节点
                for (int i = 0; i < 10; i++) {
                    List<object> timeList = new List<object>();
                    timeList.Add(new {
                        beginTime = DateTime.Now.ToString("yyyy-MM-dd"),
                        endTime = DateTime.Now.AddDays(8).ToString("yyyy-MM-dd"),
                        color = "#3286ed",
                        overtime = false,
                        text = "执行时间9天"
                    });
                    var data = new {
                        id = Guid.NewGuid().ToString(),
                        text = "计划任务" + (i + 1),
                        isexpand = false,
                        complete = false,
                        timeList = timeList,
                        hasChildren = true
                    };
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        if (data.text.IndexOf(keyword) != -1)
                        {
                            list.Add(data);
                        }
                    }
                    else
                    {
                        list.Add(data);
                    }
                }
            }
            else {
                // 子节点
                for (int i = 0; i < 2; i++)
                {
                    List<object> timeList = new List<object>();

                    if (i == 0)
                    {
                        timeList.Add(new
                        {
                            beginTime = DateTime.Now.ToString("yyyy-MM-dd"),
                            endTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"),
                            color = "#1bb99a",
                            overtime = false,
                            text = "执行时间4天"
                        });
                    }
                    else {
                        timeList.Add(new
                        {
                            beginTime = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd"),
                            endTime = DateTime.Now.AddDays(8).ToString("yyyy-MM-dd"),
                            color = "#E4474D",
                            overtime = true,
                            text = "执行时间5天"
                        });
                    }
                  
                    var data = new
                    {
                        id = Guid.NewGuid().ToString(),
                        text = "子任务" + (i + 1),
                        isexpand = false,
                        complete = true,
                        timeList = timeList,
                        hasChildren = false
                    };
                    list.Add(data);
                }
            }

            return Success(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination,string keyword)
        {
            List<object> list = new List<object>();
            // 根节点
            for (int i = 0; i < 100; i++)
            {
                List<object> timeList = new List<object>();
                timeList.Add(new
                {
                    beginTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    endTime = DateTime.Now.AddDays(8).ToString("yyyy-MM-dd"),
                    color = "#3286ed",
                    overtime = false,
                    text = "执行时间9天"
                });
                var data = new
                {
                    id = Guid.NewGuid().ToString(),
                    text = "计划任务" + (i + 1),
                    isexpand = false,
                    complete = false,
                    timeList = timeList,
                    hasChildren = true
                };
                if (!string.IsNullOrEmpty(keyword))
                {
                    if (data.text.IndexOf(keyword) != -1)
                    {
                        list.Add(data);
                    }
                }
                else
                {
                    list.Add(data);
                }
            }
            Pagination paginationobj = pagination.ToObject<Pagination>();
            int _index = (paginationobj.page - 1) * paginationobj.rows;
            int _cont = paginationobj.rows;
            if (_cont > list.Count - _index) {
                _cont = list.Count - _index;
            }

            var res =  list.GetRange(_index, _cont);
            paginationobj.records = list.Count;
            var jsonData = new
            {
                rows = res,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        #endregion
    }
}