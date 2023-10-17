using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateReason
{
    partial class HisDebateReasonTruncate : EntityBase
    {
        public HisDebateReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_REASON>();
        }

        private BridgeDAO<HIS_DEBATE_REASON> bridgeDAO;

        public bool Truncate(HIS_DEBATE_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
