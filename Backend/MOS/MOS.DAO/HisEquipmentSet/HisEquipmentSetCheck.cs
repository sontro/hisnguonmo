using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEquipmentSet
{
    partial class HisEquipmentSetCheck : EntityBase
    {
        public HisEquipmentSetCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
