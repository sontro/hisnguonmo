using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomUpdate : EntityBase
    {
        public HisPatientTypeRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ROOM> bridgeDAO;

        public bool Update(HIS_PATIENT_TYPE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
