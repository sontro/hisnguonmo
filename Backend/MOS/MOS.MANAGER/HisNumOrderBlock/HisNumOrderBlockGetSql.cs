using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockGet : BusinessBase
    {
        internal List<HisNumOrderBlockSDO> GetOccupiedStatus(HisNumOrderBlockOccupiedStatusFilter filter)
        {
            try
            {
                DateTime issueDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(filter.ISSUE_DATE).Value;
                long day = (long)issueDate.DayOfWeek + 1;

                string sql = "SELECT RT.ID AS ROOM_TIME_ID,RT.FROM_TIME AS ROOM_TIME_FROM,RT.TO_TIME AS ROOM_TIME_TO, RT.ROOM_TIME_NAME, RT.DAY, "
                            + " NOB.FROM_TIME,NOB.TO_TIME,NOB.NUM_ORDER,NOB.ID AS NUM_ORDER_BLOCK_ID, "
                            + " (CASE WHEN NOI.ID IS NOT NULL THEN 1 WHEN NOI.ID IS NULL THEN NULL END) AS IS_ISSUED "
                            + " FROM HIS_NUM_ORDER_BLOCK NOB "
                            + " JOIN HIS_ROOM_TIME RT ON RT.ID = NOB.ROOM_TIME_ID "
                            + " LEFT JOIN HIS_NUM_ORDER_ISSUE NOI ON NOI.NUM_ORDER_BLOCK_ID = NOB.ID AND NOI.ISSUE_DATE = {0} "
                            + " WHERE RT.ROOM_ID = {1} AND RT.DAY = {2} ";

                sql = string.Format(sql, filter.ISSUE_DATE, filter.ROOM_ID, day);

                if (filter.NUM_ORDER_BLOCK_ID.HasValue)
                {
                    sql += string.Format(" AND NOB.ID = {0}", filter.NUM_ORDER_BLOCK_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.FROM_TIME__FROM))
                {
                    sql += string.Format(" AND NOB.FROM_TIME >= '{0}'", filter.FROM_TIME__FROM);
                }
                if (!string.IsNullOrWhiteSpace(filter.TO_TIME__FROM))
                {
                    sql += string.Format(" AND NOB.TO_TIME >= '{0}'", filter.TO_TIME__FROM);
                }

                return DAOWorker.SqlDAO.GetSql<HisNumOrderBlockSDO>(sql);
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
