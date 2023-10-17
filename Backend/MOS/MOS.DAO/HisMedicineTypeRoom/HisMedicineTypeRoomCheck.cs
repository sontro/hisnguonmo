using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomCheck : EntityBase
    {
        public HisMedicineTypeRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
