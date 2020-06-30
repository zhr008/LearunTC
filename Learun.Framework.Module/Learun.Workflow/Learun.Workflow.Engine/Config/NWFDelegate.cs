using System.Collections.Generic;
using System.Data;

namespace Learun.Workflow.Engine
{
    /// <summary>
    /// 定义一个委托方法
    /// </summary>
    /// <param name="dbId">数据库ID</param>
    /// <param name="sql">sql语句</param>
    /// <param name="parameter">参数</param>
    public delegate DataTable DbFindTableMethod(string dbId, string sql, object parameter);
    /// <summary>
    /// 判断节点是否审核同意
    /// </summary>
    /// <param name="processId">流程实例主键</param>
    /// <param name="nodeId">流程节点Id</param>
    /// <returns></returns>
    public delegate int GetConfluenceNumMethod(string processId,string nodeId);
}
