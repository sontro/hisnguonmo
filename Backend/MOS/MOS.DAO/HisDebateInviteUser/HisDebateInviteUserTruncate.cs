using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateInviteUser
{
    partial class HisDebateInviteUserTruncate : EntityBase
    {
        public HisDebateInviteUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_INVITE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_INVITE_USER> bridgeDAO;

        public bool Truncate(HIS_DEBATE_INVITE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_INVITE_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
