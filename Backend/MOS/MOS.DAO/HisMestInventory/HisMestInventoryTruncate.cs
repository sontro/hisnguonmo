using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInventory
{
    partial class HisMestInventoryTruncate : EntityBase
    {
        public HisMestInventoryTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVENTORY>();
        }

        private BridgeDAO<HIS_MEST_INVENTORY> bridgeDAO;

        public bool Truncate(HIS_MEST_INVENTORY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_INVENTORY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
