using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUserRoom
{
    partial class HisUserRoomCheck : EntityBase
    {
        public HisUserRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ROOM>();
        }

        private BridgeDAO<HIS_USER_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
