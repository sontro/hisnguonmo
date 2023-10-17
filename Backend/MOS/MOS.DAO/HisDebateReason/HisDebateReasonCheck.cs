using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateReason
{
    partial class HisDebateReasonCheck : EntityBase
    {
        public HisDebateReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_REASON>();
        }

        private BridgeDAO<HIS_DEBATE_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
