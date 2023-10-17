using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoomGroup
{
    partial class HisRoomGroupCheck : EntityBase
    {
        public HisRoomGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_GROUP>();
        }

        private BridgeDAO<HIS_ROOM_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
