using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceCondition
{
    partial class HisServiceConditionCheck : EntityBase
    {
        public HisServiceConditionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_CONDITION>();
        }

        private BridgeDAO<HIS_SERVICE_CONDITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
