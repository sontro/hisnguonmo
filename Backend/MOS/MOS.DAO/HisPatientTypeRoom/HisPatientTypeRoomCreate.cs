using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomCreate : EntityBase
    {
        public HisPatientTypeRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ROOM> bridgeDAO;

        public bool Create(HIS_PATIENT_TYPE_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
