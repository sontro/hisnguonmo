using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.SmsSubclinicalResultNotifyThread
{
    class UpdateDataProcessor
    {
        public bool Run(List<long> ids)
        {
            try
            {
                if (ids != null && ids.Count > 0)
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_SERVICE_REQ SET IS_INFORM_RESULT_BY_SMS = 1 WHERE (IS_INFORM_RESULT_BY_SMS IS NULL OR IS_INFORM_RESULT_BY_SMS <> 1) AND %IN_CLAUSE% ", "ID");
                    return DAOWorker.SqlDAO.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
