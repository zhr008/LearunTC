using System;
using System.Collections.Generic;

namespace Learun.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class MailModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CCName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bcc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BccName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BodyText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MailFile> Attachment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
    }
}
