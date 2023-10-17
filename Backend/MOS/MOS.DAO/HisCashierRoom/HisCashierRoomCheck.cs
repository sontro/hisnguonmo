using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCashierRoom
{
    partial class HisCashierRoomCheck : EntityBase
    {
        public HisCashierRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ROOM>();
        }

        private BridgeDAO<HIS_CASHIER_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
