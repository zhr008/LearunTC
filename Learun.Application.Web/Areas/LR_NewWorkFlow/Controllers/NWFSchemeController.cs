using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Base.SystemModule;
using Learun.Application.WorkFlow;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板(新)
    /// </summary>
    public class NWFSchemeController : MvcControllerBase
    {
        private NWFSchemeIBLL nWFSchemeIBLL = new NWFSchemeBLL();
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();

        #region 视图功能
        /// <summary>
        /// 主页面
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
        /// <summary>
        /// 流程模板设计历史记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HistoryForm()
        {
            return View();
        }
        /// <summary>
        /// 预览流程模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewForm()
        {
            return View();
        }


        /// <summary>
        /// 节点信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NodeForm()
        {
            return View();
        }
        #region 审核人员添加
        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostForm()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleForm()
        {
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserForm()
        {
            return View();
        }
        /// <summary>
        /// 添加上下级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LevelForm()
        {
            return View();
        }
        /// <summary>
        /// 添加某节点执行人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuditorNodeForm()
        {
            return View();
        }
        /// <summary>
        /// 添加表单字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuditorFieldForm()
        {
            return View();
        }
        #endregion

        #region 表单添加
        /// <summary>
        /// 表单添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkformForm()
        {
            return View();
        }
        #endregion

        #region 条件字段
        /// <summary>
        /// 条件字段添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConditionFieldForm()
        {
            return View();
        }
        #endregion

        #region 按钮设置
        /// <summary>
        /// 表单添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ButtonForm()
        {
            return View();
        }
        #endregion


        /// <summary>
        /// 线段信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LineForm()
        {
            return View();
        }


        /// <summary>
        /// 导入页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetInfoPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = nWFSchemeIBLL.GetInfoPageList(paginationobj, queryJson);
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
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList()
        {
            var data = nWFSchemeIBLL.GetInfoList();
            return Success(data);
        }

        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMyInfoList(string userId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            if (userId != null && userId != userInfo.userId) {
                userInfo.userId = userId;
                userInfo.roleIds = userRelationIBLL.GetObjectIds(userId, 1);
                userInfo.postIds = userRelationIBLL.GetObjectIds(userId, 2);

            }
            var data = nWFSchemeIBLL.GetInfoList(userInfo);
            return Success(data);
        }
        /// <summary>
        /// 获取流程模板数据
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string code)
        {
            NWFSchemeInfoEntity schemeInfoEntity = nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfoEntity == null)
            {
                return Success(new { });
            }

            NWFSchemeEntity schemeEntity = nWFSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
            var nWFSchemeAuthList = nWFSchemeIBLL.GetAuthList(schemeInfoEntity.F_Id);
            var jsonData = new
            {
                info = schemeInfoEntity,
                scheme = schemeEntity,
                authList = nWFSchemeAuthList
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取模板分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemePageList(string pagination, string schemeInfoId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = nWFSchemeIBLL.GetSchemePageList(paginationobj, schemeInfoId);
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
        /// 获取流程模板数据
        /// </summary>
        /// <param name="schemeId">模板主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetScheme(string schemeId)
        {
            var data = nWFSchemeIBLL.GetSchemeEntity(schemeId);
            return Success(data);
        }

        /// <summary>
        /// 获取流程模板数据
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public void ExportScheme(string code)
        {
            NWFSchemeInfoEntity schemeInfoEntity = nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfoEntity != null)
            {
                NWFSchemeEntity schemeEntity = nWFSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
                var nWFSchemeAuthList = nWFSchemeIBLL.GetAuthList(schemeInfoEntity.F_Id);
                var jsonData = new
                {
                    info = schemeInfoEntity,
                    scheme = schemeEntity,
                    authList = nWFSchemeAuthList
                };

                string data = jsonData.ToJson();

                FileDownHelper.DownLoadString(data, schemeInfoEntity.F_Name + ".lrscheme");
            }
        }

        /// <summary>
        /// excel文件导入（通用）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExecuteImportScheme(string templateId, string fileId, int chunks, string ext)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks, userInfo);
            if (!string.IsNullOrEmpty(path))
            {
                // 读取导入文件
                string data = DirFileHelper.ReadText(path);
                // 删除临时文件
                DirFileHelper.DeleteFile2(path);
                if (!string.IsNullOrEmpty(data))
                {
                    NWFSchemeModel nWFSchemeModel = data.ToObject<NWFSchemeModel>();
                    // 验证流程编码是否重复
                    NWFSchemeInfoEntity schemeInfoEntityTmp = nWFSchemeIBLL.GetInfoEntityByCode(nWFSchemeModel.info.F_Code);
                    if (schemeInfoEntityTmp != null)
                    {
                        nWFSchemeModel.info.F_Code = Guid.NewGuid().ToString();
                    }
                    nWFSchemeIBLL.SaveEntity("", nWFSchemeModel.info, nWFSchemeModel.scheme, nWFSchemeModel.authList);

                }
                return Success("导入成功");
            }
            else
            {
                return Fail("导入模板失败!");
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfo">表单设计模板信息</param>
        /// <param name="shcemeAuth">模板权限信息</param>
        /// <param name="scheme">模板内容</param>
        /// <param name="type">类型1.正式2.草稿</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string schemeInfo, string shcemeAuth, string scheme, int type)
        {
            NWFSchemeInfoEntity schemeInfoEntity = schemeInfo.ToObject<NWFSchemeInfoEntity>();
            List<NWFSchemeAuthEntity> nWFSchemeAuthList = shcemeAuth.ToObject<List<NWFSchemeAuthEntity>>();
            NWFSchemeEntity schemeEntity = new NWFSchemeEntity();
            schemeEntity.F_Content = scheme;
            schemeEntity.F_Type = type;

            // 验证流程编码是否重复
            NWFSchemeInfoEntity schemeInfoEntityTmp = nWFSchemeIBLL.GetInfoEntityByCode(schemeInfoEntity.F_Code);
            if (schemeInfoEntityTmp != null && schemeInfoEntityTmp.F_Id != keyValue) {
                return Fail("流程编码重复");
            }

            nWFSchemeIBLL.SaveEntity(keyValue, schemeInfoEntity, schemeEntity, nWFSchemeAuthList);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            nWFSchemeIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpDateSate(string keyValue, int state)
        {
            nWFSchemeIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        /// <summary>
        /// 更新表单模板版本
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateScheme(string schemeInfoId, string schemeId)
        {
            nWFSchemeIBLL.UpdateScheme(schemeInfoId, schemeId);
            return Success("更新成功！");
        }
        #endregion

        
    }
}