using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    class SqliteDBGet
    {
        public SqliteDBGet() { }
        internal SqliteTableCreate SqliteTableCreate { get { return (SqliteTableCreate)Worker.Get<SqliteTableCreate>(); } }

        internal List<T> Get<T>(string where)
        {
            List<T> rs = null;
            try
            {
                string dataKey = typeof(T).ToString();
                string tableName = dataKey.Substring(dataKey.LastIndexOf(".") + 1);
                //if (new CacheMonitorGet().IsExistsCode(dataKey))
                //{
                try
                {
                    if (dataKey.Contains(SystemTypes.DataModel) && ProcessDuplicateData(tableName))
                    {
                        Inventec.Common.Logging.LogSystem.Warn("Bang du lieu da bi loi lap du lieu, can clearn data. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataKey), dataKey));
                        SQLiteDatabaseWorker.SQLiteDatabase.ClearTable(tableName);
                    }
                    if (!SqliteCheck.CheckExistsTable(tableName))
                    {
                        SqliteTableCreate.CreateTableNew<T>(tableName);
                    }

                    rs = SQLiteDatabaseWorker.SQLiteDatabase.GetList<T>(tableName, where);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    if (rs != null && rs.Count > 0)
                    {
                        return rs;
                    }
                    else
                    {
                        rs = null;
                        Inventec.Common.Logging.LogSystem.Warn("SqliteDBGet => GetList fail. rs == null || rs.Count == 0. Key = " + dataKey);
                    }
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn("Bang du lieu da bi thay doi ve cau truc, se tao lai bang. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataKey), dataKey));
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                    RecreateTable<T>(tableName);
                }
                //}
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return rs;
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

        void RecreateTable<T>(string tableName)
        {
            try
            {
                SQLiteDatabaseWorker.SQLiteDatabase.DropTable(tableName);
                SqliteTableCreate.CreateTableNew<T>(tableName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        bool HasColumnChangeInTable<T>(string jsondata)
        {
            bool result = false;
            try
            {
                Type type = typeof(T);
                System.Reflection.PropertyInfo[] propertyInfoOrderFields = type.GetProperties();
                if (propertyInfoOrderFields != null && propertyInfoOrderFields.Count() > 0)
                {
                    foreach (var pr in propertyInfoOrderFields)
                    {
                        if (!jsondata.Contains(String.Format("\"{0}\":", pr.Name)))
                        {
                            result = true;
                            break;
                        }
                    }
                }

                if (result)
                {
                    Inventec.Common.Logging.LogSystem.Debug("Co thay doi ve cau truc bang hoac view(them bot truong du lieu), Du lieu se duoc reset va thuc hien tai lai tu server. Key = " + type.ToString() + "____jsondata = " + jsondata);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
