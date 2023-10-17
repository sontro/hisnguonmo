using MOS.MANAGER.Base;

using Inventec.Common.Logging;
using Inventec.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn
{
    public class UpdateStatusProcessor
    {
        public bool Run(List<long> reqIds, List<long> sereservIds)
        {
            try
            {
                List<string> sqls = new List<string>();
                if (reqIds != null && reqIds.Count > 0)
                {
                    string sql = "UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, IS_UPDATED_EXT = null WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(reqIds, sql, "ID");
                    sqls.Add(sql);
                }
                if (sereservIds != null && sereservIds.Count > 0)
                {
                    string sql = "UPDATE HIS_SERE_SERV SET IS_SENT_EXT = 1 WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(sereservIds, sql, "ID");
                    sqls.Add(sql);
                }

                LogSystem.Info(LogUtil.TraceData("sqls to update serviceReqs and sereServs__:", sqls));
                if (sqls != null && sqls.Count > 0)
                {
                    return DAOWorker.SqlDAO.Execute(sqls);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return false;
        }
    }
}
