using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMrCheckSummary
{
    partial class HisMrCheckSummaryCheck : EntityBase
    {
        public HisMrCheckSummaryCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_SUMMARY>();
        }

        private BridgeDAO<HIS_MR_CHECK_SUMMARY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
