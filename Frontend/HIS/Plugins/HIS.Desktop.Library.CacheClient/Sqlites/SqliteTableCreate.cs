using Inventec.Core;
using RDCACHE.SDO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class SqliteTableCreate
    {
        const string seperate = ",";
        public SqliteTableCreate() { }

        internal bool CreateTableNew<T>(string tableName)
        {
            bool valid = true;
            SqliteDataBaseCreate sqliteDataBaseCreate = new SqliteDataBaseCreate();
            if (!SqliteCheck.CheckExistsTable(tableName))
            {
                string scriptCreateTable = "create table if not exists {0} ({1})";
                string scriptGenerateColumns = "";
                string tempGenerateColumn = "{0} {1}";

                Type type = typeof(T);
                System.Reflection.PropertyInfo[] propertyInfoOrderFields = type.GetProperties();
                if (propertyInfoOrderFields != null && propertyInfoOrderFields.Count() > 0)
                {
                    int dem = 0;
                    foreach (var pr in propertyInfoOrderFields)
                    {
                        if (!pr.PropertyType.ToString().Contains(SystemTypes.ICollection) && !pr.PropertyType.ToString().Contains(SystemTypes.DataModel))
                        {
                            scriptGenerateColumns += ((dem > 0 ? seperate : "") + String.Format(tempGenerateColumn, pr.Name, sqliteDataBaseCreate.GetSqliteType(pr.PropertyType)));
                            if (pr.PropertyType.FullName == SystemTypes.Decimal || pr.PropertyType.FullName == SystemTypes.Int16 || pr.PropertyType.FullName == SystemTypes.Int64)
                            {
                                scriptGenerateColumns += " NOT NULL";
                            }

                            dem++;
                        }
                    }
                }

                valid = sqliteDataBaseCreate.CreateTable(String.Format(scriptCreateTable, tableName, scriptGenerateColumns));
                if (valid)
                {
                    Inventec.Common.Logging.LogSystem.Info("create table " + tableName + " success");
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("create table " + tableName + " fail____" + String.Format(scriptCreateTable, tableName, scriptGenerateColumns));
                }
            }
            return valid;
        }
    }
}
