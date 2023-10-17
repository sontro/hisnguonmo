using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceRoom
{
    partial class HisServiceRoomCheck : EntityBase
    {
        public HisServiceRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_ROOM>();
        }

        private BridgeDAO<HIS_SERVICE_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
