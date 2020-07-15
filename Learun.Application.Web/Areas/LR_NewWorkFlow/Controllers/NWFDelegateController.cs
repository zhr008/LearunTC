using Learun.Application.WorkFlow;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.07
    /// 描 述：流程委托
    /// </summary>
    public class NWFDelegateController : MvcControllerBase
    {
        private NWFDelegateIBLL nWFDelegateIBLL = new NWFDelegateBLL();

        #region 视图功能
        /// <summary>
        /// 管理界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string keyword)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = nWFDelegateIBLL.GetPageList(paginationobj, keyword, userInfo);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取关联模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetRelationList(string keyValue)
        {
            var relationList = nWFDelegateIBLL.GetRelationList(keyValue);
            return Success(relationList);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">委托信息实体</param>
        /// <param name="strSchemeInfo">模板信息</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string strEntity, string strSchemeInfo)
        {
            NWFDelegateRuleEntity entity = strEntity.ToObject<NWFDelegateRuleEntity>();
            nWFDelegateIBLL.SaveEntity(keyValue, entity, strSchemeInfo.Split(','));
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteForm(string keyValue)
        {
            nWFDelegateIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult UpDateSate(string keyValue, int state)
        {
            nWFDelegateIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion
    }
}