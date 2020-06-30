using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo.QueueDemo
{
    /// <summary>
    /// 购票人信息
    /// </summary>
    [Serializable]
    public class Buyer
    {
        /// <summary>
        /// 购票人
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string id { get; set; }
    }
    /// <summary>
    /// 车票信息
    /// </summary>
    [Serializable]
    public class Ticket
    {
        /// <summary>
        /// 乘车人
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 乘车时间
        /// </summary>
        public string ticketdate { get; set; }
        /// <summary>
        /// 座位号
        /// </summary>
        public string code { get; set; }
    }
}
