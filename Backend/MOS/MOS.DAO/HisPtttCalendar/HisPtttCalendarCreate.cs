using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarCreate : EntityBase
    {
        public HisPtttCalendarCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CALENDAR>();
        }

        private BridgeDAO<HIS_PTTT_CALENDAR> bridgeDAO;

        public bool Create(HIS_PTTT_CALENDAR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_CALENDAR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
