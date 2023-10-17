using SDA.DAO.Base;
using Inventec.Backend.EFMODEL;
using Inventec.Common.Logging;
using Inventec.Core;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SDA.DAO.Sql
{
    public delegate void ParamHandler(ref object resultHolder, params OracleParameter[] parameters);

    partial class SqlExecute : EntityBase
    {
        protected const int MAX_IN_CLAUSE_SIZE = 1000;
        protected const string IN_ANCHOR = "{IN_CLAUSE}";

        public bool ExecuteStored(ParamHandler paramHanler, ref object resultHolder, string storedProcedureSql, params OracleParameter[] parameters)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(storedProcedureSql))
                {
                    using (var ctx = new AppContext())
                    {
                        OracleCommand cmd = (OracleCommand)ctx.Database.Connection.CreateCommand();
                        cmd.CommandText = storedProcedureSql;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        ctx.Database.Connection.Open();
                        cmd.ExecuteNonQuery();

                        paramHanler(ref resultHolder, parameters);

                        cmd.Dispose();
                        ctx.Database.Connection.Close();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogSystem.Error("storedProcedureSql: " + storedProcedureSql + LogUtil.TraceData("parameters:", parameters));
                result = false;
            }
            return result;
        }

        public bool Execute(string sql, params object[] parameters)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(sql))
                {
                    using (var ctx = new AppContext())
                    {
                        ctx.Database.ExecuteSqlCommand(sql, parameters);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogSystem.Error("sql: " + sql + LogUtil.TraceData("parameters:", parameters));
                result = false;
            }
            return result;
        }

        public bool Execute(List<string> sqls)
        {
            bool result = false;
            string sql = "";
            try
            {
                if (sqls != null && sqls.Count > 0)
                {
                    StringBuilder sqlBuilder = new StringBuilder();
                    foreach (string s in sqls)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            sqlBuilder.Append(string.Format("{0};", s));
                        }
                    }

                    string tmp = sqlBuilder.ToString();

                    if (!string.IsNullOrWhiteSpace(tmp))
                    {
                        sql = string.Format("BEGIN {0} END;", tmp);
                        using (var ctx = new AppContext())
                        {
                            ctx.Database.ExecuteSqlCommand(sql);
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogSystem.Error("sql: " + sql);
                result = false;
            }
            return result;
        }

        public bool Insert<T>(List<T> input)
        {
            bool result = false;
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                if (input != null && input.Count > 0 && properties != null && properties.Length > 0)
                {
                    List<string> sqls = new List<string>();
                    foreach (T data in input)
                    {
                        AppCreatorDecorator.Set<T>(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
                        CreatorDecorator.Set<T>(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
                        GroupCodeDecorator.Set<T>(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetGroupCode());
                        SDA.EFMODEL.Decorator.DummyDecorator.Set<T>(data);
                        IsActiveDecorator.Set<T>(data);
                        IsDeleteDecorator.Set<T>(data);

                        string tableName = typeof(T).Name;
                        string nameStr = "";
                        string valueStr = "";
                        foreach (PropertyInfo p in properties)
                        {
                            if (p.Name != "ID" && !p.Name.StartsWith("VIR_") && (p.GetAccessors() == null || !p.GetAccessors()[0].IsVirtual))
                            {
                                nameStr = nameStr + string.Format("{0},", p.Name);
                                if (p.GetValue(data) == null)
                                {
                                    valueStr = valueStr + string.Format("NULL,");
                                }
                                else if (p.PropertyType == typeof(string) || p.PropertyType == typeof(String))
                                {
                                    valueStr = valueStr + string.Format("'{0}',", p.GetValue(data));
                                }
                                else
                                {
                                    valueStr = valueStr + string.Format("{0},", p.GetValue(data));
                                }
                            }
                        }
                        nameStr = nameStr.Substring(0, nameStr.Length - 1);
                        valueStr = valueStr.Substring(0, valueStr.Length - 1);

                        string sql = string.Format("INSERT /*+ APPEND */ INTO {0} ({1}) VALUES ({2})", tableName, nameStr, valueStr);
                        sqls.Add(sql);
                    }
                    if (IsNotNullOrEmpty(sqls))
                    {
                        return this.Execute(sqls);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<RAW> GetSql<RAW>(string sql, params object[] parameters)
        {
            List<RAW> result = null;
            try
            {
                if (!String.IsNullOrEmpty(sql))
                {
                    using (var ctx = new AppContext())
                    {
                        result = ctx.Database.SqlQuery<RAW>(sql, parameters).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogSystem.Error("sql: " + sql + LogUtil.TraceData("parameters:", parameters));
                result = null;
            }
            return result;
        }

        public List<object> GetDynamicSql(string sql, params object[] parameters)
        {
            List<object> result = new List<object>();
            try
            {
                if (!String.IsNullOrEmpty(sql))
                {
                    using (var ctx = new AppContext())
                    {
                        var rs = ctx.Database.DynamicSqlQuery(sql, parameters);
                        foreach (object item in rs)
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogSystem.Error("sql: " + sql + LogUtil.TraceData("parameters:", parameters));
                result = null;
            }
            return result;
        }

        public RAW GetSqlSingle<RAW>(string sql, params object[] parameters)
        {
            RAW result = default(RAW);
            try
            {
                if (!String.IsNullOrEmpty(sql))
                {
                    using (var ctx = new AppContext())
                    {
                        result = ctx.Database.SqlQuery<RAW>(sql, parameters).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("sql: " + sql + LogUtil.TraceData("parameters:", parameters));
                LogSystem.Error(ex);
                result = default(RAW);
            }
            return result;
        }

        /// <summary>
        /// Tra ve chuoi string co dang: IN (id1, id2, ...) or IN (id1001, id1002, ...)
        /// Luu y: trong menh de IN ko qua 1000 phan tu
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string AddInClause(List<long> elements, string query, string property)
        {
            string inClause;
            if (IsNotNullOrEmpty(elements))
            {
                inClause = "(" + property + " IN (";
                int size = elements.Count;
                int i = 0;
                do
                {
                    size--;
                    if (++i == MAX_IN_CLAUSE_SIZE)
                    {
                        inClause += elements[size] + ")";
                        if (size > 0)
                        {
                            inClause += " OR " + property + " IN (";
                        }
                        i = 0;
                    }
                    else
                    {
                        inClause += elements[size] + ",";
                    }
                } while (size > 0);
                inClause = inClause.Substring(0, inClause.Length - 1) + "))";
            }
            else
            {
                inClause = " 1 = 0 ";
            }
            return query.Replace(IN_ANCHOR, inClause);
        }

        /**
         * Bo sung menh de NOT IN trong cau truy van
         */
        public string AddNotInClause(List<long> elements, string query, string property)
        {
            string notInClause;

            if (IsNotNullOrEmpty(elements))
            {
                notInClause = "(" + property + " NOT IN (";
                int size = elements.Count;
                int i = 0;
                do
                {
                    size--;
                    if (++i == MAX_IN_CLAUSE_SIZE)
                    {
                        notInClause = elements[size] + ")";
                        if (size > 0)
                        {
                            notInClause += " AND " + property + " NOT IN (";
                        }
                        i = 0;
                    }
                    else
                    {
                        notInClause += elements[size] + ",";
                    }
                } while (size > 0);
                notInClause = notInClause.Substring(0, notInClause.Length - 1) + "))";
            }
            else
            {
                notInClause = " 1 = 1 ";
            }
            return query.Replace(IN_ANCHOR, notInClause);
        }

    }
}
