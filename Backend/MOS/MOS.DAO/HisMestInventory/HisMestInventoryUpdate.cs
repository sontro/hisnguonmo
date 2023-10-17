using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInventory
{
    partial class HisMestInventoryUpdate : EntityBase
    {
        public HisMestInventoryUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVENTORY>();
        }

        private BridgeDAO<HIS_MEST_INVENTORY> bridgeDAO;

        public bool Update(HIS_MEST_INVENTORY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_INVENTORY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
