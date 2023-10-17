using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccAppointment
{
    partial class HisVaccAppointmentUpdate : EntityBase
    {
        public HisVaccAppointmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_APPOINTMENT>();
        }

        private BridgeDAO<HIS_VACC_APPOINTMENT> bridgeDAO;

        public bool Update(HIS_VACC_APPOINTMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACC_APPOINTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
