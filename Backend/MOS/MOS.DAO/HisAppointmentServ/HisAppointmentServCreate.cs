using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentServ
{
    partial class HisAppointmentServCreate : EntityBase
    {
        public HisAppointmentServCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_APPOINTMENT_SERV>();
        }

        private BridgeDAO<HIS_APPOINTMENT_SERV> bridgeDAO;

        public bool Create(HIS_APPOINTMENT_SERV data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_APPOINTMENT_SERV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
