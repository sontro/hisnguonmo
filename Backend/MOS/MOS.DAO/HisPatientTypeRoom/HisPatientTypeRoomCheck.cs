using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomCheck : EntityBase
    {
        public HisPatientTypeRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
