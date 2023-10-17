using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccAppointment
{
    partial class HisVaccAppointmentTruncate : EntityBase
    {
        public HisVaccAppointmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_APPOINTMENT>();
        }

        private BridgeDAO<HIS_VACC_APPOINTMENT> bridgeDAO;

        public bool Truncate(HIS_VACC_APPOINTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACC_APPOINTMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
