using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyCheck : EntityBase
    {
        public HisEquipmentSetMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET_MATY>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
