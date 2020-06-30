using System;
using System.Collections.Generic;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeModel
    {
        /// <summary>
        /// 开始方式1配置完立即执行2根据设置的开始时间
        /// </summary>
        public int startType { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startTime { get; set; }
        /// <summary>
        /// 结束方法 1无限期2有结束时间
        /// </summary>
        public int endType { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endTime { get; set; }

        /// <summary>
        /// 执行频率类别 
        /// 1：只执行一次
        /// 2：简单重复执行 涉及分钟，小时，天，周
        /// 3：明细频率设置 
        /// 4：表达式设置 corn表达式
        /// </summary>
        public int executeType { get; set; }


        /// <summary>
        /// 间隔时间值 对应2
        /// </summary>
        public int simpleValue { get; set; }
        /// <summary>
        /// 间隔类型 对应2 minute分hours小时day天week周
        /// </summary>
        public string simpleType { get; set; }

        /// <summary>
        /// 间隔类型 对应3 频率明显
        /// </summary>
        public List<DetailFrequencyModel> frequencyList { get; set; }

        /// <summary>
        /// cron表达式 对应4
        /// </summary>
        public string cornValue { get; set; }


        /// <summary>
        /// 是否重启1是0不是
        /// </summary>
        public int isRestart { get; set; }
        /// <summary>
        /// 间隔重启时间（分钟）
        /// </summary>
        public int restartMinute { get; set; }
        /// <summary>
        /// 重启次数
        /// </summary>
        public int restartNum { get; set; }
        /// <summary>
        /// 方法类型1sql 2存储过程 3接口 4ioc依赖注入
        /// </summary>
        public int methodType { get; set; }
        /// <summary>
        /// 数据ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        public string strSql { get; set; }
        /// <summary>
        /// 存储过程
        /// </summary>
        public string procName { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 接口请求方式 1get 2post
        /// </summary>
        public string urlType { get; set; }
        /// <summary>
        /// 依赖注入方法名
        /// </summary>
        public string iocName { get; set; }
    }


    /// <summary>
    /// 明细频率类
    /// </summary>
    public class DetailFrequencyModel
    {
        /// <summary>
        /// 小时
        /// </summary>
        public string hour { get; set; }
        /// <summary>
        /// 分钟
        /// </summary>
        public string minute { get; set; }
        /// <summary>
        /// 间隔类型 每天day，每周week，每月month
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///间隔执行值
        /// </summary>
        public string carryDate { get; set; }
        /// <summary>
        /// 执行月
        /// </summary>
        public string carryMounth { get; set; }
    }
}
