using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodCheck : EntityBase
    {
        public HisAppointmentPeriodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_PERIOD>();
        }

        private BridgeDAO<HIS_APPOINTMENT_PERIOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
