using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDepositReason
{
    partial class HisDepositReasonCheck : EntityBase
    {
        public HisDepositReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REASON>();
        }

        private BridgeDAO<HIS_DEPOSIT_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
