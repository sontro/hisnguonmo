using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisReceptionRoom
{
    partial class HisReceptionRoomCheck : EntityBase
    {
        public HisReceptionRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RECEPTION_ROOM>();
        }

        private BridgeDAO<HIS_RECEPTION_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
