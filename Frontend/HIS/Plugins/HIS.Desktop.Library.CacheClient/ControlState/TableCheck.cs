using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient.ControlState
{
    public class TableCheck
    {
        public static bool ExistsDataInTable(string table)
        {
            bool result = false;
            try
            {
                //Thread.Sleep(200);
                string cmd = "select count(*) from " + table + ";";
                DataTable oldData = DatabaseCSWorker.DatabaseCS.GetDataTable(cmd);
                result = (oldData != null && oldData.Rows.Count > 0 && Convert.ToInt64(oldData.Rows[0][0].ToString()) > 0);

                if (!result)
                {
                    Inventec.Common.Logging.LogSystem.Info("cmd = " + cmd + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => oldData), oldData));
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("ExistsKeyInTable: table " + table + ", result = " + result);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static bool ExistsKeyInTable(string table, string keyName, string value)
        {
            bool result = false;
            try
            {
                Thread.Sleep(200);
                string cmd = "select count(*) from " + table + " where " + keyName + " = '" + value + "';";
                DataTable oldData = DatabaseCSWorker.DatabaseCS.GetDataTable(cmd);
                result = (oldData != null && oldData.Rows.Count > 0 && Convert.ToInt64(oldData.Rows[0][0].ToString()) > 0);

                if (!result)
                {
                    Inventec.Common.Logging.LogSystem.Info("cmd = " + cmd + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => oldData), oldData));
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("ExistsKeyInTable: table " + table + ", result = " + result);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
            
        public static bool CheckExistsTable(string tableName)
        {
            bool valid = false;
            try
            {
                DataTable tables = DatabaseCSWorker.DatabaseCS.GetDataTable("select NAME from SQLITE_MASTER where type='table' and NAME = '" + tableName + "'");
                valid = (tables != null && tables.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }


    }
}
