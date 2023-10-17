using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestRoom
{
    partial class HisMestRoomCheck : EntityBase
    {
        public HisMestRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_ROOM>();
        }

        private BridgeDAO<HIS_MEST_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
