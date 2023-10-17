using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.SqlExecute
{
    public class SqlExecutor: BusinessBase
    {		
        internal SqlExecutor()
            : base()
        {

        }

        internal SqlExecutor(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(ExecuteSqlSDO sdo)
        {
            bool result = false;
            try
            {
                bool valid = true;
                SDA_SQL sdaSql = null;
                List<SDA_SQL_PARAM> sqlParams = null;

                SqlExecuteCheck checker = new SqlExecuteCheck(param);
                valid = valid && checker.IsValid(sdo, ref sdaSql, ref sqlParams);

                if (valid)
                {
                    string sql = sdaSql.CONTENT;
                    if (IsNotNullOrEmpty(sqlParams))
                    {
                        sqlParams = sqlParams.OrderBy(o => o.PARAM_ORDER).ToList();

                        object[] pars = new object[sqlParams.Count];
                        int i = 0;
                        
                        string paramStr = "";

                        foreach (SDA_SQL_PARAM p in sqlParams)
                        {
                            pars[i] = sdo.SqlParams.Where(o => o.SqlParamId == p.ID).FirstOrDefault().Value;
                            paramStr += string.Format("param{0}: {1};", i, pars[i].ToString());
                            i++;
                        }

                        result = DAOWorker.SqlDAO.Execute(sql, pars);

                        if (result)
                        {
                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.SdaSql_ThucThiSqlThanhCong, sql, paramStr).Run();
                        }
                        else
                        {
                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.SdaSql_ThucThiSqlThatBai, sql, paramStr).Run();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
