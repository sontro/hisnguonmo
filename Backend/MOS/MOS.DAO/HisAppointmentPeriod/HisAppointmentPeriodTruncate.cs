using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodTruncate : EntityBase
    {
        public HisAppointmentPeriodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_PERIOD>();
        }

        private BridgeDAO<HIS_APPOINTMENT_PERIOD> bridgeDAO;

        public bool Truncate(HIS_APPOINTMENT_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
