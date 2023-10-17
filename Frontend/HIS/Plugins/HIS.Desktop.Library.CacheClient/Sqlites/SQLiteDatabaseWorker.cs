using Inventec.Common.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Library.CacheClient
{
    public class SQLiteDatabaseWorker
    {
        private static SQLiteDatabase sQLiteDatabase;
        public static SQLiteDatabase SQLiteDatabase
        {
            get
            {
                if (sQLiteDatabase == null)
                {
                    try
                    {
                        sQLiteDatabase = new SQLiteDatabase(String.Format("{0}\\{1}",
                            Application.StartupPath, @"DB\HIS\HIS.SqliteDBLocal.db"));
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
                return sQLiteDatabase;
            }
            set
            {
                sQLiteDatabase = value;
            }
        }        
    }
}
