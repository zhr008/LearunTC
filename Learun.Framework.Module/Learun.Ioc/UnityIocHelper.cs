using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Learun.Ioc
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.03
    /// 描 述：UnityIoc
    /// </summary>
    public class UnityIocHelper
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="containerName">容器名称</param>
        private UnityIocHelper(string containerName)
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            _container = new UnityContainer();
            if (containerName == "IOCcontainer" || section.Containers[containerName] != null)
            {
                section.Configure(_container, containerName);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 容器
        /// </summary>
        private readonly IUnityContainer _container;
        /// <summary>
        /// 容器实例
        /// </summary>
        private static readonly UnityIocHelper instance = new UnityIocHelper("IOCcontainer");

        /// <summary>
        /// 容器实例
        /// </summary>
        private static readonly UnityIocHelper wfInstance = new UnityIocHelper("WfIOCcontainer");

        /// <summary>
        /// 容器实例
        /// </summary>
        private static readonly UnityIocHelper tsInstance = new UnityIocHelper("TsIOCcontainer");

        /// <summary>
        /// UnityIoc容器实例
        /// </summary>
        public static UnityIocHelper Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// 流程
        /// </summary>
        public static UnityIocHelper WfInstance
        {
            get { return wfInstance; }
        }
        /// <summary>
        /// 定时任务
        /// </summary>
        public static UnityIocHelper TsInstance
        {
            get { return tsInstance; }
        }
        #endregion

        #region 获取对应接口的具体实现类
        /// <summary>
        /// 获取实现类(默认映射)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>接口</returns>
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// 获取实现类(默认映射)带参数的
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="parameter">参数</param>
        /// <returns>接口</returns>
        public T GetService<T>(params ParameterOverride[] parameter)
        {
            return _container.Resolve<T>(parameter);
        }
        /// <summary>
        /// 获取实现类(指定映射)带参数的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns>接口</returns>
        public T GetService<T>(string name, params ParameterOverride[] parameter)
        {
            return _container.Resolve<T>(name, parameter);
        }
        #endregion

        #region 判断接口是否被注册了
        /// <summary>
        /// 判断接口是否被实现了
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>bool</returns>
        public bool IsResolve<T>()
        {
            return _container.IsRegistered<T>();
        }
        /// <summary>
        /// 判断接口是否被实现了
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="name">映射名称</param>
        /// <returns></returns>
        public bool IsResolve<T>(string name)
        {
            return _container.IsRegistered<T>(name);
        }
        #endregion
    }
}
