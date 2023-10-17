using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRepayReason
{
    partial class HisRepayReasonCheck : EntityBase
    {
        public HisRepayReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPAY_REASON>();
        }

        private BridgeDAO<HIS_REPAY_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
