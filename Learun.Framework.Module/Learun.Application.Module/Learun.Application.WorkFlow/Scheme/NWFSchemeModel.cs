using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板,导入模型(新)
    /// </summary>
    public class NWFSchemeModel
    {
        /// <summary>
        /// 模板基础信息
        /// </summary>
        public NWFSchemeInfoEntity info { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public NWFSchemeEntity scheme { get; set; }
        /// <summary>
        /// 模板权限信息
        /// </summary>
        public List<NWFSchemeAuthEntity> authList { get; set; }
    }
}
