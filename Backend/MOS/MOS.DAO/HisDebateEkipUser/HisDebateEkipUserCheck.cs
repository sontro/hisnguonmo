using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateEkipUser
{
    partial class HisDebateEkipUserCheck : EntityBase
    {
        public HisDebateEkipUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_EKIP_USER>();
        }

        private BridgeDAO<HIS_DEBATE_EKIP_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
