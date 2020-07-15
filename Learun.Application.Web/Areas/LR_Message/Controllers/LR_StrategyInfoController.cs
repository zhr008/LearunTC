using Learun.Application.Message;
using Learun.Application.Organization;
using Learun.Util;
using System;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_Message.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public class LR_StrategyInfoController : MvcControllerBase
    {
        private LR_StrategyInfoIBLL lR_StrategyInfoIBLL = new LR_StrategyInfoBLL();
        private UserIBLL userIBLL = new UserBLL();
        #region 视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        /// <summary>
        /// 消息发送界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendForm()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetList( string queryJson )
        {
            var data = lR_StrategyInfoIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = lR_StrategyInfoIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFormData(string keyValue)
        {
            var data = lR_StrategyInfoIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteForm(string keyValue)
        {
            lR_StrategyInfoIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue,LR_MS_StrategyInfoEntity entity)
        {
            lR_StrategyInfoIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 验证策略编码是否重复
        /// </summary>
        /// <param name="keyValue">策略主键</param>
        /// <param name="F_StrategyCode">策略编码</param>
        /// <returns></returns>
        public ActionResult ExistStrategyCode(string keyValue, string F_StrategyCode)
        {
            bool res = lR_StrategyInfoIBLL.ExistStrategyCode(keyValue, F_StrategyCode);
            return Success(res);
        }
        /// <summary>
        /// 策略消息发送
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <param name="content">消息内容</param>
        /// <param name="userlist">用户列表</param>
        /// <returns></returns>
        public ActionResult SendMessage(string code,string content,string userlist)
        {
            try
            {
                var data = userIBLL.GetListByUserIds(userlist);
                ResParameter resParameter= lR_StrategyInfoIBLL.SendMessage(code, content, data.ToJson());
                if (resParameter.code.ToString() == "fail")
                {
                    return Fail(resParameter.info);
                }
                else
                {
                    return Success(resParameter.info);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}
