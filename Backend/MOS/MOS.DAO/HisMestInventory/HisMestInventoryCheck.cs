using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestInventory
{
    partial class HisMestInventoryCheck : EntityBase
    {
        public HisMestInventoryCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVENTORY>();
        }

        private BridgeDAO<HIS_MEST_INVENTORY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
