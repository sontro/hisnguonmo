using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoomTime
{
    partial class HisRoomTimeCheck : EntityBase
    {
        public HisRoomTimeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TIME>();
        }

        private BridgeDAO<HIS_ROOM_TIME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
