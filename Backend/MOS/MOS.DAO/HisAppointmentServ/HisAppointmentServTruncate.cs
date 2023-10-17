using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAppointmentServ
{
    partial class HisAppointmentServTruncate : EntityBase
    {
        public HisAppointmentServTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_SERV>();
        }

        private BridgeDAO<HIS_APPOINTMENT_SERV> bridgeDAO;

        public bool Truncate(HIS_APPOINTMENT_SERV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_APPOINTMENT_SERV> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
