using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateReason
{
    partial class HisDebateReasonUpdate : EntityBase
    {
        public HisDebateReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_REASON>();
        }

        private BridgeDAO<HIS_DEBATE_REASON> bridgeDAO;

        public bool Update(HIS_DEBATE_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBATE_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
