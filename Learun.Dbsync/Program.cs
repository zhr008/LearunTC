using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Dbsync
{
    class Program
    {
        /// <summary>
        /// 同步两个数据库
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Db db = new Db();

            db.SyncData();


            Console.Read();
        }
    }
}
