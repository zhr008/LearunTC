using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：功能模块控制器
    /// </summary>
    public class ModuleController : MvcControllerBase
    {
        #region 模块对象
        private ModuleIBLL moduleIBLL = new ModuleBLL();
        #endregion

        #region 视图
        /// <summary>
        /// 功能模块管理视图
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

        #region 功能模块
        /// <summary>
        /// 获取功能模块数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetModuleList()
        {
            var data = moduleIBLL.GetModuleList();
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetModuleTree()
        {
            var data = moduleIBLL.GetModuleTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetModuleCheckTree()
        {
            var data = moduleIBLL.GetModuleCheckTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetExpendModuleTree()
        {
            var data = moduleIBLL.GetExpendModuleTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取列表数据根据父级id
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <param name="type">功能类型</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetModuleListByParentId(string keyword, string parentId)
        {
            var jsondata = moduleIBLL.GetModuleListByParentId(keyword, parentId);
            return this.Success(jsondata);
        }

        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetCheckTree()
        {
            var moduleList = moduleIBLL.GetModuleCheckTree();
            var buttonList = moduleIBLL.GetButtonCheckTree();
            var columnList = moduleIBLL.GetColumnCheckTree();
            var formList = moduleIBLL.GetFormCheckTree();


            var jsonData = new
            {
                moduleList,
                buttonList,
                columnList,
                formList
            };

            return this.Success(jsonData);
        }
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取功能模块按钮数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetButtonListNoAuthorize(string moduleId)
        {
            var data = moduleIBLL.GetButtonListNoAuthorize(moduleId);
            return this.Success(data);
        }
        /// <summary>
        /// 获取功能模块按钮数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetButtonList(string moduleId)
        {
            var data = moduleIBLL.GetButtonList(moduleId);
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetButtonCheckTree()
        {
            var data = moduleIBLL.GetButtonCheckTree();
            return this.Success(data);
        }
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取功能模块视图数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetColumnList(string moduleId)
        {
            var data = moduleIBLL.GetColumnList(moduleId);
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetColumnCheckTree()
        {
            var data = moduleIBLL.GetColumnCheckTree();
            return this.Success(data);
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetFormData(string keyValue)
        {
            var module = moduleIBLL.GetModuleEntity(keyValue);
            var btns = moduleIBLL.GetButtonList(keyValue);
            var cols = moduleIBLL.GetColumnList(keyValue);
            var fields = moduleIBLL.GetFormList(keyValue);
            var jsondata = new
            {
                moduleEntity = module,
                moduleButtons = btns,
                moduleColumns = cols,
                moduleFields = fields
            };
            return this.Success(jsondata);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">功能实体</param>
        /// <param name="moduleButtonListJson">按钮实体列表</param>
        /// <param name="moduleColumnListJson">视图实体列表</param>
        /// <param name="moduleFormListJson">表单字段列表</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string keyValue, string moduleEntityJson, string moduleButtonListJson, string moduleColumnListJson, string moduleFormListJson)
        {
            var moduleButtonList = moduleButtonListJson.ToList<ModuleButtonEntity>();
            var moduleColumnList = moduleColumnListJson.ToList<ModuleColumnEntity>();
            var moduleFormList = moduleFormListJson.ToList<ModuleFormEntity>();
            var moduleEntity = moduleEntityJson.ToObject<ModuleEntity>();

            moduleIBLL.SaveEntity(keyValue, moduleEntity, moduleButtonList, moduleColumnList, moduleFormList);
            return Success("保存成功。");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult DeleteForm(string keyValue)
        {
            bool res = moduleIBLL.Delete(keyValue);
            if (res)
            {
                return Success("删除成功。");
            }
            else
            {
                return Fail("有子节点无法删除。");
            }
        }
        #endregion

        #region 权限数据
        /// <summary>
        /// 获取权限按钮和列表信息
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetAuthorizeButtonColumnList(string url)
        {
            Dictionary<string, string> dicButton = new Dictionary<string, string>();
            Dictionary<string, string> dicColumn = new Dictionary<string, string>();
            Dictionary<string, string> dicForm = new Dictionary<string, string>();

            ModuleEntity moduleEntity = moduleIBLL.GetModuleByUrl(url);

            if (moduleEntity != null)
            {
                List<ModuleButtonEntity> buttonList = moduleIBLL.GetButtonList(moduleEntity.F_ModuleId);
                foreach (var item in buttonList)
                {
                    if (!dicButton.ContainsKey(item.F_EnCode))
                    {
                        dicButton.Add(item.F_EnCode, item.F_FullName);
                    }
                }
                List<ModuleColumnEntity> columnList = moduleIBLL.GetColumnList(moduleEntity.F_ModuleId);
                foreach (var item in columnList)
                {
                    if (!dicColumn.ContainsKey(item.F_EnCode.ToLower()))
                    {
                        dicColumn.Add(item.F_EnCode.ToLower(), item.F_FullName);
                    }
                }
                List<ModuleFormEntity> formList = moduleIBLL.GetFormList(moduleEntity.F_ModuleId);
                foreach (var item in formList)
                {
                    if (item.F_EnCode != null && !dicForm.ContainsKey(item.F_EnCode))
                    {
                        dicForm.Add(item.F_EnCode, item.F_FullName);
                    }
                }

            }
            var jsonData = new
            {
                module = moduleEntity,
                btns = dicButton,
                cols = dicColumn,
                forms = dicForm
            };
            return this.Success(jsonData);
        }
        #endregion
    }
}