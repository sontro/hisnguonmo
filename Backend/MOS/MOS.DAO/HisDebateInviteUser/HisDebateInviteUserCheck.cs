using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateInviteUser
{
    partial class HisDebateInviteUserCheck : EntityBase
    {
        public HisDebateInviteUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_INVITE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_INVITE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
