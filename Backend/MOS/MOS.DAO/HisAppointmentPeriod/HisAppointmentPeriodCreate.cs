using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodCreate : EntityBase
    {
        public HisAppointmentPeriodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_PERIOD>();
        }

        private BridgeDAO<HIS_APPOINTMENT_PERIOD> bridgeDAO;

        public bool Create(HIS_APPOINTMENT_PERIOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
