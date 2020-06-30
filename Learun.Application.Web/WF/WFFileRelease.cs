using Learun.Application.Base.Files;
using Learun.Application.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learun.Application.Web
{
    public class WFFileRelease: IWorkFlowMethod
    {
        private FileInfoIBLL fileInfoIBLL = new FileInfoBLL();

        /// <summary>
        /// 流程执行
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(WfMethodParameter parameter) {
            fileInfoIBLL.UpdateEntity(parameter.processId);
        }
    }
}