using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomCheck : EntityBase
    {
        public HisTreatmentBedRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BED_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_BED_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
