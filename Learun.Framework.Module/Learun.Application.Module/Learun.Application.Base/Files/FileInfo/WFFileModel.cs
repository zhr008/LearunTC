using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.Base.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class WFFileModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime F_CreateDate { get; set; }
        /// <summary>
        /// 流程实例主键
        /// </summary>
        public string  F_Id { get; set; }
        /// <summary>
        /// 流程模板主键
        /// </summary>
        public string F_SchemeId{ get; set; }
        /// <summary>
        /// 流程模板编码
        /// </summary>
        public string F_SchemeCode{ get; set; }
        /// <summary>
        /// 流程模板名称
        /// </summary>
        public string F_SchemeName{ get; set; }
        /// <summary>
        /// 自定义标题
        /// </summary>
        public string F_Title{ get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        public string F_Level{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_EnabledMark{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_IsAgain{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_IsFinished{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_IsChild{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_ParentTaskId{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_ParentProcessId{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_CreateUserId{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_CreateUserName{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_IsStart{ get; set; }


        /// <summary>
        /// 任务名称
        /// </summary>
        public string F_TaskName { get; set; }
        /// <summary>
        /// 任务主键
        /// </summary>
        public string F_TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int? F_TaskType { get; set; }

        /// <summary> 
        /// 是否被催办 1 被催办了
        /// </summary> 
        /// <returns></returns> 
        public int? F_IsUrge { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件编码
        /// </summary>
        public string FileCode { get; set; }
        /// <summary>
        /// 文件版本
        /// </summary>
        public string FileVer { get; set; }
    }
}
