using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal List<D_HIS_SERVICE_REQ_2> GetDHisServiceReq2(DHisServiceReq2Filter filter)
        {
            try
            {
                StringBuilder query = new StringBuilder("SELECT * FROM D_HIS_SERVICE_REQ_2 WHERE 1 = 1 ");
                List<object> listParam = new List<object>();
                if (filter.TREATMENT_ID.HasValue)
                {
                    query.Append(" AND TREATMENT_ID = :param1");
                    listParam.Add(filter.TREATMENT_ID.Value);
                }
                if (filter.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    query.Append(" AND REQUEST_DEPARTMENT_ID = :param2");
                    listParam.Add(filter.REQUEST_DEPARTMENT_ID.Value);
                }

                return DAOWorker.SqlDAO.GetSql<D_HIS_SERVICE_REQ_2>(query.ToString(), listParam.ToArray());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisServiceReqGroupByDateSDO> GetGroupByDate(long treatmentId)
        {
            try
            {
                string query =
                    "SELECT INTRUCTION_DATE AS InstructionDate, TREATMENT_ID as TreatmentId, count(id) as Total "
                    + " FROM HIS_SERVICE_REQ "
                    + " WHERE TREATMENT_ID = :param1 AND IS_DELETE = 0 "
                    + " GROUP by INTRUCTION_DATE, TREATMENT_ID";
                return DAOWorker.SqlDAO.GetSql<HisServiceReqGroupByDateSDO>(query, treatmentId);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal long GetCount(HisServiceReqCountFilter filter)
        {
            try
            {
                StringBuilder query = new StringBuilder("SELECT COUNT(1) FROM HIS_SERVICE_REQ WHERE 1 = 1 ");
                List<object> listParam = new List<object>();
                if (!string.IsNullOrWhiteSpace(filter.EXECUTE_LOGINNAME))
                {
                    query.Append(" AND EXECUTE_LOGINNAME = :param1");
                    listParam.Add(filter.EXECUTE_LOGINNAME);
                }
                if (filter.EXECUTE_ROOM_ID.HasValue)
                {
                    query.Append(" AND EXECUTE_ROOM_ID = :param2");
                    listParam.Add(filter.EXECUTE_ROOM_ID.Value);
                }
                if (filter.INTRUCTION_DATE.HasValue)
                {
                    query.Append(" AND INTRUCTION_DATE = :param3");
                    listParam.Add(filter.INTRUCTION_DATE.Value);
                }
                if (filter.EXE_DESK_ID.HasValue)
                {
                    query.Append(" AND EXE_DESK_ID = :param4");
                    listParam.Add(filter.EXE_DESK_ID.Value);
                }
                if (filter.IS_BHYT.HasValue && filter.IS_BHYT.Value)
                {
                    query.Append(" AND TDL_HEIN_CARD_NUMBER IS NOT NULL");
                }
                if (filter.IS_BHYT.HasValue && !filter.IS_BHYT.Value)
                {
                    query.Append(" AND TDL_HEIN_CARD_NUMBER IS NULL");
                }
                if (filter.SERVICE_REQ_STT_IDs != null)
                {
                    query.AppendFormat(" AND SERVICE_REQ_STT_ID IN ({0})", string.Join(",", filter.SERVICE_REQ_STT_IDs));
                }
                if (filter.SERVICE_REQ_TYPE_IDs != null)
                {
                    query.AppendFormat(" AND SERVICE_REQ_TYPE_ID IN ({0})", string.Join(",", filter.SERVICE_REQ_TYPE_IDs));
                }
                
                return DAOWorker.SqlDAO.GetSqlSingle<long>(query.ToString(), listParam.ToArray());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return 0;
            }
        }

        internal List<HisServiceReqMaxNumOrderSDO> GetMaxNumOrder(HisServiceReqMaxNumOrderFilter filter)
        {
            try
            {
                StringBuilder query = new StringBuilder("SELECT MAX(NUM_ORDER) AS MAX_NUM_ORDER, EXECUTE_ROOM_ID FROM HIS_SERVICE_REQ WHERE ");
                List<object> listParam = new List<object>();

                query.Append(" INTRUCTION_DATE = :param1");
                listParam.Add(filter.INSTRUCTION_DATE);

                if (filter.EXECUTE_ROOM_IDs != null)
                {
                    query.AppendFormat(" AND EXECUTE_ROOM_ID IN ({0})", string.Join(",", filter.EXECUTE_ROOM_IDs));
                }
                if (filter.SERVICE_REQ_STT_IDs != null)
                {
                    query.AppendFormat(" AND SERVICE_REQ_STT_ID IN ({0})", string.Join(",", filter.SERVICE_REQ_STT_IDs));
                }
				if (filter.IS_PRIORITY.HasValue && filter.IS_PRIORITY.Value)
                {
                    query.Append(" AND PRIORITY IS NOT NULL AND PRIORITY <> 0");
                }
				if (filter.IS_PRIORITY.HasValue && !filter.IS_PRIORITY.Value)
                {
                    query.Append(" AND (PRIORITY IS NULL OR PRIORITY = 0)");
                }

                query.Append(" GROUP BY EXECUTE_ROOM_ID");

                return DAOWorker.SqlDAO.GetSql<HisServiceReqMaxNumOrderSDO>(query.ToString(), listParam.ToArray());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
