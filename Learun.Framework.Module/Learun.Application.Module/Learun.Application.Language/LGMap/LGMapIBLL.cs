using Learun.Util;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Language
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public interface LGMapIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取语言包映射关系数据集合
        /// <param name="Code">语言包编码</param>
        /// <param name="isMain">是否是主语言</param>
        /// <summary>
        /// <returns></returns>
        Dictionary<string, string> GetMap(string Code, bool isMain);
        /// <summary>
        /// 获取列表数据
        /// <param name="TypeCode">编码</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGMapEntity> GetListByTypeCode(string TypeCode);
        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGMapEntity> GetList(string queryJson);
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <param name="typeList">语言类型列表</param>
        /// <summary>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson, string typeList);
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LGMapEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据名称获取列表
        /// <param name="keyValue">F_Name</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGMapEntity> GetListByName(string keyValue);
        /// <summary>
        /// 根据名称与类型获取列表
        /// <param name="keyValue">F_Name</param>
        /// <param name="typeCode">typeCode</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LGMapEntity> GetListByNameAndType(string keyValue, string typeCode);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">F_Code</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string nameList, string newNameList, string code);
        #endregion

    }
}
