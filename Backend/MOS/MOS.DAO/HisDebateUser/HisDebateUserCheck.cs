using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateUser
{
    partial class HisDebateUserCheck : EntityBase
    {
        public HisDebateUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
