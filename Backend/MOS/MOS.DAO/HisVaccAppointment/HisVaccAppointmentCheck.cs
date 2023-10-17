using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccAppointment
{
    partial class HisVaccAppointmentCheck : EntityBase
    {
        public HisVaccAppointmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_APPOINTMENT>();
        }

        private BridgeDAO<HIS_VACC_APPOINTMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
