using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoomSaro
{
    partial class HisRoomSaroCheck : EntityBase
    {
        public HisRoomSaroCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_SARO>();
        }

        private BridgeDAO<HIS_ROOM_SARO> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
