using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodGet : BusinessBase
    {
        internal List<HisAppointmentPeriodCountByDateSDO> GetCountByDate(HisAppointmentPeriodCountByDateFilter filter)
        {
            try
            {
                string query =
                    "SELECT AP.ID, AP.MAXIMUM, AP.TO_HOUR, AP.TO_MINUTE, AP.FROM_HOUR, AP.FROM_MINUTE, A.APPOINTMENT_DATE, AP.BRANCH_ID, NVL(C_COUNT, 0) AS CURRENT_COUNT "
                    + " FROM HIS_APPOINTMENT_PERIOD AP "
                    + " LEFT JOIN ( "
                    + "     SELECT APPOINTMENT_PERIOD_ID, COUNT(T.ID) AS C_COUNT, APPOINTMENT_DATE, BRANCH_ID "
                    + "     FROM HIS_TREATMENT T "
                    + "     WHERE T.APPOINTMENT_DATE = :param1 AND T.BRANCH_ID = :param2 "
                    + "     GROUP BY APPOINTMENT_PERIOD_ID, APPOINTMENT_DATE, BRANCH_ID) A ON A.APPOINTMENT_PERIOD_ID = AP.ID "
                    + "     WHERE AP.IS_ACTIVE = 1 AND AP.BRANCH_ID = :param3";
                return DAOWorker.SqlDAO.GetSql<HisAppointmentPeriodCountByDateSDO>(query, filter.APPOINTMENT_DATE, filter.BRANCH_ID, filter.BRANCH_ID);
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
