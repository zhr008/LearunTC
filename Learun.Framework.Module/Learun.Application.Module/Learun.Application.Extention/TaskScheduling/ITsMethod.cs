namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2019.01.09
    /// 描 述：任务调度器执行的方法需要继承的接口
    /// </summary>
    public interface ITsMethod
    {
        /// <summary>
        /// 任务调度器执行的方法
        /// </summary>
        void Execute();
    }
}
