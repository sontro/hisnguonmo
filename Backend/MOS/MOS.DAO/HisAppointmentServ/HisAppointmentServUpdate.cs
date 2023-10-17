using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAppointmentServ
{
    partial class HisAppointmentServUpdate : EntityBase
    {
        public HisAppointmentServUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_SERV>();
        }

        private BridgeDAO<HIS_APPOINTMENT_SERV> bridgeDAO;

        public bool Update(HIS_APPOINTMENT_SERV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_APPOINTMENT_SERV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
