using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarUpdate : EntityBase
    {
        public HisPtttCalendarUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CALENDAR>();
        }

        private BridgeDAO<HIS_PTTT_CALENDAR> bridgeDAO;

        public bool Update(HIS_PTTT_CALENDAR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_CALENDAR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
