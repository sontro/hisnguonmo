using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoomType
{
    partial class HisRoomTypeCheck : EntityBase
    {
        public HisRoomTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
