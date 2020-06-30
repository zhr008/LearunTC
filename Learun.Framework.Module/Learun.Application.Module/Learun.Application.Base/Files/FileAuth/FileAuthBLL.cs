using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件权限管理 
    /// </summary> 
    public class FileAuthBLL: FileAuthIBLL
    {
        private FileAuthService fileAuthService = new FileAuthService();


        #region 获取数据
        /// <summary>
        /// 判断当前文件夹有没有上传权限
        /// </summary>
        /// <param name="id">文件夹主键</param>
        /// <returns></returns>
        public bool IsUPLoad(string id)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            if (userInfo.isSystem)
            {
                return true;
            }
            else {
                if (string.IsNullOrEmpty(userInfo.roleIds))
                {
                    return false;
                }
                else {
                    var roleid = "," + userInfo.roleIds + ",";
                    var list = fileAuthService.GetFList(id);
                    foreach(var item in list) {
                        if (roleid.IndexOf("," + item.F_ObjId + ",") != -1) {
                            if (item.F_AuthType.IndexOf("2") != -1) {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
        }
        /// <summary>
        /// 获取授权信息列表
        /// </summary>
        /// <param name="F_FileInfoId">文件信息主键</param>
        /// <returns></returns>
        public IEnumerable<FileAuthEntity> GetList(string F_FileInfoId)
        {
            return fileAuthService.GetList(F_FileInfoId);
        }

        /// <summary>
        /// 实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FileAuthEntity GetEntity(string keyValue)
        {
            return fileAuthService.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            fileAuthService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        public bool SaveEntity(string keyValue, FileAuthEntity entity)
        {
           return  fileAuthService.SaveEntity(keyValue, entity);
        }
        #endregion
    }
}
