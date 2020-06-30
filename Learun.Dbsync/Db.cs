using Learun.Application.Base.SystemModule;
using Learun.Application.Extention.DisplayBoardManage;
using Learun.Application.Report;
using Learun.Application.WorkFlow;
using Learun.DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Dbsync
{
    public class Db:RepositoryFactory
    {
        /// <summary>
        /// 同步两个数据库间的数据
        /// </summary>
        public void SyncData() {
            var todb = this.BaseRepository("ToDb").BeginTrans();

            // lr_kbconfiginfo lr_nwf_scheme lr_rpt_fileinfo
            try
            {
                var list = this.BaseRepository("FromDb").FindList<LR_KBConfigInfoEntity>();
                todb.ExecuteBySql(" Delete from  lr_kbconfiginfo");
                foreach (var item in list) {
                    todb.Insert(item);
                }
                var list2 = this.BaseRepository("FromDb").FindList<NWFSchemeEntity>();
                todb.ExecuteBySql(" Delete from  lr_nwf_scheme");
                foreach (var item2 in list2)
                {
                    todb.Insert(item2);
                }

                var list3 = this.BaseRepository("FromDb").FindList<LR_RPT_FileInfoEntity>();
                todb.ExecuteBySql(" Delete from  lr_rpt_fileinfo");
                foreach (var item3 in list3)
                {
                    todb.Insert(item3);
                }


                todb.Commit();
            }
            catch (Exception)
            {
                todb.Rollback();
            }

            Console.WriteLine("同步完成");

            


        }
    }
}
