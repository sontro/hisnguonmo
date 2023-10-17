using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentRoom
{
    partial class HisTreatmentRoomCheck : EntityBase
    {
        public HisTreatmentRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
