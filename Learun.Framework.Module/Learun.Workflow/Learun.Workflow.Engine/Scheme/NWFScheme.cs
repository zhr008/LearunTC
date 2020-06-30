using System.Collections.Generic;

namespace Learun.Workflow.Engine
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.09
    /// 描 述：工作流模板模型
    /// </summary>
    public class NWFScheme
    {
        /// <summary>
        /// 节点数据
        /// </summary>
        public List<NWFNodeInfo> nodes { get; set; }
        /// <summary>
        /// 线条数据
        /// </summary>
        public List<NWFLineInfo> lines { get; set; }
        /// <summary>
        /// 流程撤销作废的时候执行的方法
        /// </summary>
        public NWFCloseDo closeDo { get; set; }
    }
}
