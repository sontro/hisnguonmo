using Inventec.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient.Sqlites
{
    class Checker
    {
        internal SqliteTableCreate SqliteTableCreate { get { return (SqliteTableCreate)Worker.Get<SqliteTableCreate>(); } }
        internal bool ValidTable<T>(string dataKey)
        {
            bool valid = true;
            string tableName = dataKey.Substring(dataKey.LastIndexOf(".") + 1);
            if (!SqliteCheck.CheckExistsTable(tableName))
            {
                SqliteTableCreate.CreateTableNew<T>(tableName); 
                valid = false;
            }
            if (dataKey.Contains(SystemTypes.DataModel) && ProcessDuplicateData(tableName))
            {
                Inventec.Common.Logging.LogSystem.Warn("Bang du lieu da bi loi lap du lieu, can clearn data. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataKey), dataKey));
                SQLiteDatabaseWorker.SQLiteDatabase.ClearTable(tableName);
                valid = false;
            }
            return valid;
        }

        bool ProcessDuplicateData(string tableName)
        {
            bool rs = false;
            try
            {
                var tbSearch = SQLiteDatabaseWorker.SQLiteDatabase.GetDataTable("SELECT ID, COUNT(*) c FROM " + tableName + " GROUP BY ID HAVING c > 1;");
                if (tbSearch != null && tbSearch.Rows.Count > 0)
                {
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                rs = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return rs;
        }
    }
}
