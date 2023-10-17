using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEkipPlan
{
    partial class HisEkipPlanCheck : EntityBase
    {
        public HisEkipPlanCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN>();
        }

        private BridgeDAO<HIS_EKIP_PLAN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
