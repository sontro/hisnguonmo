using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccAppointment
{
    partial class HisVaccAppointmentCreate : EntityBase
    {
        public HisVaccAppointmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_APPOINTMENT>();
        }

        private BridgeDAO<HIS_VACC_APPOINTMENT> bridgeDAO;

        public bool Create(HIS_VACC_APPOINTMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACC_APPOINTMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
