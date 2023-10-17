using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateUser
{
    partial class HisDebateUserTruncate : EntityBase
    {
        public HisDebateUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_USER> bridgeDAO;

        public bool Truncate(HIS_DEBATE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
