using Learun.Application.Language;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public class LGMapController : MvcControllerBase
    {
        private LGMapIBLL lGMapIBLL = new LGMapBLL();

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
        /// 数据字典语言
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DataItemLG()
        {
            return View();
        }
        /// <summary>
        /// 系统功能语言
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SystemModuleLG()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// <param name="TypeCode">编码</param>
        /// <summary>
        /// <returns></returns>
        public ActionResult GetListByTypeCode(string TypeCode)
        {
            var data = lGMapIBLL.GetListByTypeCode(TypeCode);
            return Success(data);
        }
        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetList(string queryJson)
        {
            var data = lGMapIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetPageList(string pagination, string queryJson, string typeList)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = lGMapIBLL.GetPageList(paginationobj, queryJson, typeList);
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
            var data = lGMapIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 根据名称获取列表
        /// <param name="keyValue">F_Name</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetListByName(string keyValue)
        {
            var data = lGMapIBLL.GetListByName(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 根据名称与类型获取列表
        /// <param name="keyValue">F_Name</param>
        /// <param name="typeCode">typeCode</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetListByNameAndType(string keyValue, string typeCode)
        {
            var data = lGMapIBLL.GetListByNameAndType(keyValue, typeCode);
            return Success(data);
        }
        /// <summary>
        /// 根据语言类型编码获取语言包
        /// </summary>
        /// <param name="typeCode">语言类型编码</param>
        /// <param name="ver">版本号</param>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetLanguageByCode(string typeCode,string ver,bool isMain) {
            var data = lGMapIBLL.GetMap(typeCode, isMain);
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
            lGMapIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">F_Code</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult SaveForm(string nameList, string newNameList, string code)
        {
            lGMapIBLL.SaveEntity(nameList, newNameList, code);
            return Success("保存成功！");
        }
        #endregion

    }
}