using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Organization;
using Learun.Util;
using Learun.Util.Operat;
using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private DataAuthorizeIBLL dataAuthorizeIBLL = new DataAuthorizeBLL();
        private FilterMode _customMode;
        private UserIBLL userBll = new UserBLL();
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerLoginAttribute(FilterMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // 登录拦截是否忽略
            if (_customMode == FilterMode.Ignore)
            {
                return;
            }

            var request = filterContext.HttpContext.Request;


            // 用户win客户端的打开
            string user = request.QueryString["user"];
            string pwd = request.QueryString["pwd"];
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pwd))
            {
                UserEntity userEntity = userBll.CheckLogin(user, pwd);
                if (userEntity.LoginOk) {
                    OperatorHelper.Instance.AddLoginUser(user, "Learun_ADMS_6.1_PC", null);//写入缓存信息
                }
            }


            //免密登录示例
            /*string token = request.QueryString["token"];
            string account2 = request.QueryString["account"];
            if (!string.IsNullOrEmpty(token))
            {

                OperatorHelper.Instance.AddLoginUser(account2, "Learun_ADMS_6.1_PC", null);//写入缓存信息
            }
            */
            
            string account = "";

            if (!request.Headers["account"].IsEmpty())
            {
                account = request.Headers["account"].ToString();
            }


            var areaName = filterContext.RouteData.DataTokens["area"] + "/";            //获取当前区域
            var controllerName = filterContext.RouteData.Values["controller"] + "/";    //获取控制器
            var action = filterContext.RouteData.Values["Action"];                      //获取当前Action
            string currentUrl = "/" + areaName + controllerName + action;               //拼接构造完整url
            WebHelper.AddHttpItems("currentUrl", currentUrl);

            var _currentUrl = WebHelper.GetHttpItems("currentUrl");
            if (_currentUrl.IsEmpty())
            {
                WebHelper.AddHttpItems("currentUrl", currentUrl);
            }
            else
            {
                WebHelper.UpdateHttpItem("currentUrl", currentUrl);
            }

            // 验证登录状态
            int res = OperatorHelper.Instance.IsOnLine(account).stateCode;
            if (res != 1)// 登录过期或者未登录
            {
                if (res == 2)
                {
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.nologin, info = "other" }.ToJson() };
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Login/Index?error=other");
                    }
                    return;

                }



                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.nologin, info = "nologin" }.ToJson() };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                }
                return;
            }
            // IP过滤
            if (!this.FilterIP())
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.nologin, info = "noip" }.ToJson() };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?error=ip");
                }
                return;
            }
            // 时段过滤
            if (!this.FilterTime())
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.nologin, info = "notime" }.ToJson() };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?error=time");
                }
                return;
            }

            // 判断当前接口是否需要加载数据权限
            if (!this.DataAuthorize(currentUrl))
            {
                filterContext.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.fail, info = "没有该数据权限" }.ToJson() };
                return;
            }
        }
        /// <summary>
        /// IP过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterIP()
        {
            bool isFilterIP = Config.GetValue("FilterIP").ToBool();
            if (isFilterIP == true)
            {
                return new FilterIPBLL().FilterIP();
            }
            return true;
        }
        /// <summary>
        /// 时段过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterTime()
        {
            bool isFilterIP = Config.GetValue("FilterTime").ToBool();
            if (isFilterIP == true)
            {
                return new FilterTimeBLL().FilterTime();
            }
            return true;
        }
        /// <summary>
        /// 执行权限认证
        /// </summary>
        /// <param name="currentUrl">当前连接</param>
        /// <returns></returns>
        private bool DataAuthorize(string currentUrl)
        {
            return dataAuthorizeIBLL.SetWhereSql(currentUrl, false);
        }
    }
}