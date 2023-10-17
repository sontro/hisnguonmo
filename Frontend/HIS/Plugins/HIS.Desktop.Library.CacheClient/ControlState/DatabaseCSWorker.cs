using Inventec.Common.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Library.CacheClient.ControlState
{
    public class DatabaseCSWorker
    {
        private static SQLiteDatabase sDatabaseCS;
        public static SQLiteDatabase DatabaseCS
        {
            get
            {
                if (sDatabaseCS == null)
                {
                    try
                    {
                        sDatabaseCS = new SQLiteDatabase(String.Format("{0}\\Integrate\\ControlState\\DB\\CSDB.db",
                            Application.StartupPath));
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
                return sDatabaseCS;
            }
            set
            {
                sDatabaseCS = value;
            }
        }
    }
}
