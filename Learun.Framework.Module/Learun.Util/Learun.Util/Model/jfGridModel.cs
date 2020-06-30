using System.Collections.Generic;

namespace Learun.Util
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.07.10
    /// 描 述：表格属性模型
    /// </summary>
    public class jfGridModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hidden { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<jfGridModel> children { get; set; }
    }
}
