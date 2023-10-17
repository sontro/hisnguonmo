using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFinancePeriod
{
    partial class HisFinancePeriodCheck : EntityBase
    {
        public HisFinancePeriodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FINANCE_PERIOD>();
        }

        private BridgeDAO<HIS_FINANCE_PERIOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
