using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoomTypeModule
{
    partial class HisRoomTypeModuleCheck : EntityBase
    {
        public HisRoomTypeModuleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE_MODULE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE_MODULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
