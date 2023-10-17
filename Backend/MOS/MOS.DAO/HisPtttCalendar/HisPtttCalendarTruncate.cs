using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarTruncate : EntityBase
    {
        public HisPtttCalendarTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CALENDAR>();
        }

        private BridgeDAO<HIS_PTTT_CALENDAR> bridgeDAO;

        public bool Truncate(HIS_PTTT_CALENDAR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_CALENDAR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
