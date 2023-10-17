using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestInventory
{
    partial class HisMestInventoryCreate : EntityBase
    {
        public HisMestInventoryCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVENTORY>();
        }

        private BridgeDAO<HIS_MEST_INVENTORY> bridgeDAO;

        public bool Create(HIS_MEST_INVENTORY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_INVENTORY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
