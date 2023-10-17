using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarCheck : EntityBase
    {
        public HisPtttCalendarCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CALENDAR>();
        }

        private BridgeDAO<HIS_PTTT_CALENDAR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
