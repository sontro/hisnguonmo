using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathCheck : EntityBase
    {
        public HisEventsCausesDeathCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EVENTS_CAUSES_DEATH>();
        }

        private BridgeDAO<HIS_EVENTS_CAUSES_DEATH> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
