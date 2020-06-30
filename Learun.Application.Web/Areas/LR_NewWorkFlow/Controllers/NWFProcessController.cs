using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Organization;
using Learun.Application.WorkFlow;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.09
    /// 描 述：流程进程
    /// </summary>
    public class NWFProcessController : MvcControllerBase
    {
        private NWFProcessIBLL nWFProcessIBLL = new NWFProcessBLL();

        private NWFSchemeIBLL nWFSchemeIBLL = new NWFSchemeBLL();
        private NWFTaskIBLL nWFTaskIBLL = new NWFTaskBLL();

        private UserIBLL userIBLL = new UserBLL();
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();

        #region 视图功能
        /// <summary>
        /// 视图功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReleaseForm()
        {
            return View();
        }
        /// <summary>
        /// 流程容器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NWFContainerForm()
        {
            return View();
        }
        /// <summary>
        /// 查看节点审核信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LookNodeForm()
        {
            return View();
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateForm()
        {
            return View();
        }
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuditFlowForm()
        {
            return View();
        }
        /// <summary>
        /// 加签审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignAuditFlowForm()
        {
            return View();
        }
        /// <summary>
        /// 加签审核
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignFlowForm() {
            return View();
        }

        /// <summary>
        /// 流程进度查看（父子流程）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LookFlowForm()
        {
            return View();
        }

        /// <summary>
        /// 监控页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MonitorIndex()
        {
            return View();
        }
        /// <summary>
        /// 监控详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MonitorDetailsIndex()
        {
            return View();
        }
        /// <summary>
        /// 查看各个节点表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MonitorForm()
        {
            return View();
        }
        /// <summary>
        /// 指定审核人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppointForm()
        {
            return View();
        }
        /// <summary>
        /// 添加审核节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddTaskForm()
        {
            return View();
        }
        /// <summary>
        /// 批量审核页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchAuditIndex()
        {
            return View();
        }

        /// <summary>
        /// 选择下一节点审核人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectUserForm() {
            return View();
        }
        /// <summary>
        /// 签名弹层
        /// </summary>
        /// <returns></returns>
        public ActionResult SignForm() {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPorcessList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list  = nWFProcessIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = list,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTaskPageList(string pagination, string queryJson, string categoryId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            IEnumerable<NWFProcessEntity> list = new List<NWFProcessEntity>();

            UserInfo userInfo = LoginUserInfo.Get();
            switch (categoryId)
            {
                case "1":
                    list = nWFProcessIBLL.GetMyPageList(userInfo.userId, paginationobj, queryJson);
                    break;
                case "2":
                    list = nWFProcessIBLL.GetMyTaskPageList(userInfo, paginationobj, queryJson);
                    break;
                case "3":
                    list = nWFProcessIBLL.GetMyFinishTaskPageList(userInfo, paginationobj, queryJson);
                    break;
            }

            var jsonData = new
            {
                rows = list,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取批量审核任务清单
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBatchTaskPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            UserInfo userInfo = LoginUserInfo.Get();
            var data = nWFProcessIBLL.GetMyTaskPageList(userInfo, paginationobj, queryJson,true);
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
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTask(string taskId)
        {
            var data = nWFTaskIBLL.GetEntity(taskId);
            return Success(data);
        }
        #endregion

        #region 保存更新删除
        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteEntity(string processId)
        {
            nWFProcessIBLL.DeleteEntity(processId);
            return Success("删除成功");
        }
        #endregion

        #region 流程API
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemeByCode(string code)
        {
            var schemeInfo = nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfo != null) {
                var data = nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                return Success(data);
            }
            return Fail("找不到该流程模板");
        }
        /// <summary>
        /// 根据流程进程主键获取流程模板
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemeByProcessId(string processId)
        {
            var processEntity = nWFProcessIBLL.GetEntity(processId);
            if (processEntity != null) {
                if (string.IsNullOrEmpty(processEntity.F_SchemeId))
                {
                    var schemeInfo = nWFSchemeIBLL.GetInfoEntityByCode(processEntity.F_SchemeCode);
                    if (schemeInfo != null)
                    {
                        var data = nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                        return Success(data);
                    }
                }
                else {
                    var data = nWFSchemeIBLL.GetSchemeEntity(processEntity.F_SchemeId);
                    return Success(data);
                }
            }
            return Fail("找不到该流程模板");
        }

        /// <summary>
        /// 获取流程下一节点审核
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="operationCode">操作编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetNextAuditors(string code, string processId, string taskId, string nodeId, string operationCode)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = nWFProcessIBLL.GetNextAuditors(code, processId, taskId, nodeId, operationCode, userInfo);
            return Success(data);
        }

        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProcessDetails(string processId, string taskId) {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = nWFProcessIBLL.GetProcessDetails(processId, taskId, userInfo);
            if (!string.IsNullOrEmpty(data.childProcessId))
            {
                processId = data.childProcessId;
            }

            var taskNode = nWFProcessIBLL.GetTaskUserList(processId);

            var jsonData = new
            {
                info = data,
                task = taskNode
            };

            return Success(jsonData);
        }

        /// <summary>
        /// 获取子流程详细信息
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="taskId">父流程子流程发起主键</param>
        /// <param name="schemeCode">子流程流程模板编码</param>
        /// <param name="nodeId">父流程发起子流程节点Id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetChildProcessDetails(string processId,string taskId, string schemeCode,string nodeId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = nWFProcessIBLL.GetChildProcessDetails(processId, taskId, schemeCode, nodeId, userInfo);
            var taskNode = nWFProcessIBLL.GetTaskUserList(data.childProcessId);
            var jsonData = new
            {
                info = data,
                task = taskNode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveDraft(string processId, string schemeCode, string createUserId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            if (!string.IsNullOrEmpty(createUserId) && userInfo.userId != createUserId)
            {
                var userEntity = userIBLL.GetEntityByUserId(createUserId);
                userInfo.userId = userEntity.F_UserId;
                userInfo.enCode = userEntity.F_EnCode;
                userInfo.realName = userEntity.F_RealName;
                userInfo.nickName = userEntity.F_NickName;
                userInfo.headIcon = userEntity.F_HeadIcon;
                userInfo.gender = userEntity.F_Gender;
                userInfo.mobile = userEntity.F_Mobile;
                userInfo.telephone = userEntity.F_Telephone;
                userInfo.email = userEntity.F_Email;
                userInfo.oICQ = userEntity.F_OICQ;
                userInfo.weChat = userEntity.F_WeChat;
                userInfo.companyId = userEntity.F_CompanyId;
                userInfo.departmentId = userEntity.F_DepartmentId;
                userInfo.openId = userEntity.F_OpenId;
                userInfo.isSystem = userEntity.F_SecurityLevel == 1 ? true : false;
            }

            nWFProcessIBLL.SaveDraft(processId, schemeCode, userInfo);
            return Success("流程草稿保存成功");
        }
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteDraft(string processId) {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.DeleteDraft(processId, userInfo);
            return Success("流程草稿删除成功");
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="title">流程进程自定义标题</param>
        /// <param name="level">流程进程等级</param>
        /// <param name="auditors">下一节点审核人</param>
        /// <param name="createUserId">流程创建人</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateFlow(string schemeCode, string processId,string title,int level,string auditors,string createUserId)
        {

            UserInfo userInfo = LoginUserInfo.Get();
            if (!string.IsNullOrEmpty(createUserId) && userInfo.userId != createUserId) {
                if (title == null) {
                    title = "";
                }
                title += "【" + userInfo.realName + "代发】";
                var userEntity = userIBLL.GetEntityByUserId(createUserId);
                userInfo.userId = userEntity.F_UserId;
                userInfo.enCode = userEntity.F_EnCode;
                userInfo.realName = userEntity.F_RealName;
                userInfo.nickName = userEntity.F_NickName;
                userInfo.headIcon = userEntity.F_HeadIcon;
                userInfo.gender = userEntity.F_Gender;
                userInfo.mobile = userEntity.F_Mobile;
                userInfo.telephone = userEntity.F_Telephone;
                userInfo.email = userEntity.F_Email;
                userInfo.oICQ = userEntity.F_OICQ;
                userInfo.weChat = userEntity.F_WeChat;
                userInfo.companyId = userEntity.F_CompanyId;
                userInfo.departmentId = userEntity.F_DepartmentId;
                userInfo.openId = userEntity.F_OpenId;
                userInfo.isSystem = userEntity.F_SecurityLevel == 1 ? true : false;

                userInfo.roleIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 1);
                userInfo.postIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 2);
            }
            try
            {
                nWFProcessIBLL.CreateFlow(schemeCode, processId, title, level, auditors, userInfo);
                return Success("流程创建成功");
            }
            catch (System.Exception ex)
            {
                nWFProcessIBLL.SaveDraft(processId, schemeCode, userInfo);
                return Fail(ex.Message);
            }
          
        }

        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="parentProcessId">父流程进程主键</param>
        /// <param name="parentTaskId">父流程任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateChildFlow(string schemeCode, string processId,string parentProcessId, string parentTaskId) {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.CreateChildFlow(schemeCode, processId, parentProcessId, parentTaskId, userInfo);
            return Success("流程创建成功");
        }

        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AgainCreateFlow(string processId)
        {
            try
            {
                UserInfo userInfo = LoginUserInfo.Get();
                nWFProcessIBLL.AgainCreateFlow(processId, userInfo);
                return Success("流程重新创建成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
           
        }
        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">流程审批操作码agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="operationName">流程审批操名称</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">审批意见</param>
        /// <param name="auditors">下一节点指定审核人</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditFlow(string operationCode, string operationName, string processId, string taskId, string des, string auditors,string stamp, string signUrl)
        {
            UserInfo userInfo = LoginUserInfo.Get();

            try
            {
                nWFProcessIBLL.AuditFlow(operationCode, operationName, processId, taskId, des, auditors, stamp, signUrl, userInfo);
                return Success("流程审批成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
           
        }
        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">流程审批操作码agree 同意 disagree 不同意</param>
        /// <param name="taskIds">任务串</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditFlows(string operationCode, string taskIds)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.AuditFlows(operationCode, taskIds, userInfo);
            return Success("流程批量审批成功");
        }
        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userId">加签人员</param>
        /// <param name="des">加签说明</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SignFlow(string processId, string taskId, string userId, string des) {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.SignFlow(processId, taskId, userId, des, userInfo);
            return Success("流程加签成功");
        }
        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核操作码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">加签说明</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SignAuditFlow(string operationCode, string processId, string taskId, string des) {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.SignAuditFlow(operationCode,processId, taskId, des, userInfo);
            return Success("加签审批成功");
        }

        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReferFlow(string processId, string taskId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.ReferFlow(processId, taskId, userInfo);
            return Success("保存成功");
        }

        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UrgeFlow(string processId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.UrgeFlow(processId, userInfo);
            return Success("催办成功");
        }
        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult RevokeFlow(string processId) {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.RevokeFlow(processId, userInfo);
            return Success("撤销成功");
        }

        /// <summary>
        /// 获取流程当前任务需要处理的人
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTaskUserList(string processId)
        {
            var data = nWFProcessIBLL.GetTaskUserList(processId);
            return Success(data);
        }
        /// <summary>
        /// 指派流程审核人
        /// </summary>
        /// <param name="strList">任务列表</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult AppointUser(string strList)
        {
            IEnumerable<NWFTaskEntity> list = strList.ToObject<IEnumerable<NWFTaskEntity>>();
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.AppointUser(list, userInfo);
            return Success("指派成功");
        }
        /// <summary>
        /// 作废流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteFlow(string processId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.DeleteFlow(processId, userInfo);
            return Success("作废成功");
        }


        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult RevokeAudit(string processId,string taskId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var res = nWFProcessIBLL.RevokeAudit(processId, taskId, userInfo);
            if (res)
            {
                return Success("撤销成功");
            }
            else {
                return Fail("撤销失败，当前不允许撤销！");
            }
           
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="bNodeId"></param>
        /// <param name="eNodeId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AddTask(string processId, string bNodeId,string eNodeId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            nWFProcessIBLL.AddTask(processId, bNodeId, eNodeId, userInfo);
            return Success("添加成功");
        }
        #endregion
    }
}