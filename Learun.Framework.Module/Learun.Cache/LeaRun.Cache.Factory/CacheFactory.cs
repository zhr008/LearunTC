using Learun.Cache.Base;
using Learun.Cache.Redis;

namespace Learun.Cache.Factory
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.06
    /// 描 述：定义缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// 获取缓存实例
        /// </summary>
        /// <returns></returns>
        public static ICache CaChe()
        {
            return new CacheByRedis();
        }
    }
}
