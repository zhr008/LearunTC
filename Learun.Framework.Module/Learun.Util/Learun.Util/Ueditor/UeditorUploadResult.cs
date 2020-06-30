using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Util.Ueditor
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：百度编辑器UE上传返回结果
    /// </summary>
    public class UeditorUploadResult
    {
        /// <summary>
        /// 
        /// </summary>
        public UeditorUploadState State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OriginFileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum UeditorUploadState
    {
        /// <summary>
        /// 
        /// </summary>
        Success = 0,
        /// <summary>
        /// 
        /// </summary>
        SizeLimitExceed = -1,
        /// <summary>
        /// 
        /// </summary>
        TypeNotAllow = -2,
        /// <summary>
        /// 
        /// </summary>
        FileAccessError = -3,
        /// <summary>
        /// 
        /// </summary>
        NetworkError = -4,
        /// <summary>
        /// 
        /// </summary>
        Unknown = 1,
    }
}
