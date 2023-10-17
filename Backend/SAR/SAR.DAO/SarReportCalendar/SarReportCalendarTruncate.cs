using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportCalendar
{
    partial class SarReportCalendarTruncate : EntityBase
    {
        public SarReportCalendarTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_CALENDAR>();
        }

        private BridgeDAO<SAR_REPORT_CALENDAR> bridgeDAO;

        public bool Truncate(SAR_REPORT_CALENDAR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_REPORT_CALENDAR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
