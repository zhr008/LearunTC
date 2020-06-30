using Learun.Application.TwoDevelopment.SystemDemo;
using Learun.Util;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learun.Application.WebApi.Modules.Demo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2020.01.02
    /// 描 述：系统表单-请假单
    /// </summary>
    public class WfSystemDemo:BaseApi
    {
        DemoleaveIBLL demoleaveIBLL = new DemoleaveBLL();
        /// <summary>
        /// 注册接口
        /// </summary>
        public WfSystemDemo()
            : base("/learun/adms/demo/wfsys")
        {
            Get["/form"] = GetForm;
            Post["save"] = SaveForm;
        }



        #region 获取数据
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetForm(dynamic _)
        {
            string processId = this.GetReqData();
            var data = demoleaveIBLL.GetEntity(processId);
            return Success(data);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存订单表单（新增、修改）
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response SaveForm(dynamic _)
        {
            ReqFormEntity parameter = this.GetReqData<ReqFormEntity>();
            var demoleaveEntity = parameter.strEntity.ToObject<DemoleaveEntity>();
            demoleaveIBLL.SaveEntity(parameter.keyValue, demoleaveEntity);
            return Success("保存成功。");
        }
        #endregion



        /// <summary>
        /// 表单实体类
        /// </summary>
        private class ReqFormEntity
        {
            public string keyValue { get; set; }
            public string strEntity { get; set; }
        }
    }
}