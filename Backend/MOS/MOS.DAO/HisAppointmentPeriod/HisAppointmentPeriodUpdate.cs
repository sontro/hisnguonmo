using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodUpdate : EntityBase
    {
        public HisAppointmentPeriodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_PERIOD>();
        }

        private BridgeDAO<HIS_APPOINTMENT_PERIOD> bridgeDAO;

        public bool Update(HIS_APPOINTMENT_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
