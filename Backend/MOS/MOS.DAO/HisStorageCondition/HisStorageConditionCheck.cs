using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisStorageCondition
{
    partial class HisStorageConditionCheck : EntityBase
    {
        public HisStorageConditionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STORAGE_CONDITION>();
        }

        private BridgeDAO<HIS_STORAGE_CONDITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
