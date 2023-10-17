using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRoom
{
    partial class HisRoomCheck : EntityBase
    {
        public HisRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM>();
        }

        private BridgeDAO<HIS_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
