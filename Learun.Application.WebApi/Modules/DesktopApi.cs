using Learun.Application.AppMagager;
using Learun.Application.Base.Desktop;
using Learun.Application.Base.SystemModule;
using Learun.Util;
using Nancy;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.WebApi.Modules
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.21
    /// 描 述：桌面配置接口
    /// </summary>
    public class DesktopApi: BaseApi
    {
        public DesktopApi()
        : base("/learun/adms/desktop")
        {
            Get["/setting"] = GetMap;
            Get["/data"] = GetSqlData;
            Get["/imgid"] = GetImgList;
            Get["/img"] = GetImg;

        }


        private DTTargetIBLL dTTargetIBLL = new DTTargetBLL();
        private DTListIBLL dTListIBLL = new DTListBLL();
        private DTChartIBLL dTChartIBLL = new DTChartBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();

        private DTImgIBLL dTImgIBLL = new DTImgBLL();


        /// <summary>
        /// 获取桌面配置信息
        /// <summary>
        /// <returns></returns>
        public Response GetMap(dynamic _)
        {
            string ver = this.GetReqData();// 获取模板请求数据

            var target = dTTargetIBLL.GetList();
            var list = dTListIBLL.GetList();
            var chart = dTChartIBLL.GetList();

            var data = new
            {
                target,
                list,
                chart
            };
            string md5 = Md5Helper.Encrypt(data.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = data,
                    ver = md5
                };
                return Success(jsondata);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetSqlData(dynamic _)
        {
            reqModel req = this.GetReqData<reqModel>();// 获取模板请求数据
            switch (req.type)
            {
                case "Target":
                    var data = dTTargetIBLL.GetEntity(req.id);
                    if (data != null && !string.IsNullOrEmpty(data.F_Sql))
                    {
                        DataTable dt = databaseLinkIBLL.FindTable(data.F_DataSourceId, data.F_Sql);
                        var jsonData2 = new
                        {
                            Id = req.id,
                            value = dt.Rows[0][0]
                        };
                        return Success(jsonData2);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            Id = req.id,
                            value = ""
                        };
                        return Success(jsonData);
                    }
                case "chart":
                    var chartData = dTChartIBLL.GetEntity(req.id);
                    if (chartData != null && !string.IsNullOrEmpty(chartData.F_Sql))
                    {
                        DataTable dt = databaseLinkIBLL.FindTable(chartData.F_DataSourceId, chartData.F_Sql);
                        var jsonData2 = new
                        {
                            Id = req.id,
                            value = dt
                        };
                        return Success(jsonData2);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            Id= req.id
                        };
                        return Success(jsonData);
                    }
                case "list":
                    var listdata = dTListIBLL.GetEntity(req.id);
                    if (listdata != null && !string.IsNullOrEmpty(listdata.F_Sql))
                    {
                        DataTable dt = databaseLinkIBLL.FindTable(listdata.F_DataSourceId, listdata.F_Sql);
                        var jsonData2 = new
                        {
                            Id = req.id,
                            value = dt
                        };
                        return Success(jsonData2);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            Id = req.id
                        };
                        return Success(jsonData);
                    }
            }

            return Success(new
            {
                Id = req.id
            });
        }

        /// <summary>
        /// 获取桌首页图片
        /// <summary>
        /// <returns></returns>
        public Response GetImgList(dynamic _)
        {

            var list = dTImgIBLL.GetList();
            List<string> res = new List<string>();
            foreach (var item in list) {
                res.Add(item.F_Id);
            }
            return Success(res);
        }

        /// <summary>
        /// 获取人员头像图标
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetImg(dynamic _)
        {
            string keyValue = this.GetReqData();// 获取模板请求数据
            dTImgIBLL.GetImg(keyValue);
            return Success("获取成功");
        }

        private class reqModel {
            /// <summary>
            /// 类型
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// 主键值
            /// </summary>
            public string id { get; set; }
        }
    }
    
}