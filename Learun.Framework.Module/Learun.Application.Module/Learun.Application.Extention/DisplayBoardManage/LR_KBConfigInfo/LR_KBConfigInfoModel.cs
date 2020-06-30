namespace Learun.Application.Extention.DisplayBoardManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息数据模型
    /// </summary>
    public class ConfigInfoModel {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public string modelType { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 类型 1 sql语句 2 接口
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// sql 语句
        /// </summary>
        public string sql { get; set; }
        /// <summary>
        /// 接口url地址
        /// </summary>
        public string url { get; set; }
    }
    public class ConfigInfoDataModel
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 类型 1 sql语句 2 接口
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public string modelType { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        public object data { get; set; }
    }
}
