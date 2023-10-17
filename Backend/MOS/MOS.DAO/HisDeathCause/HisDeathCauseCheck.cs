using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDeathCause
{
    partial class HisDeathCauseCheck : EntityBase
    {
        public HisDeathCauseCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CAUSE>();
        }

        private BridgeDAO<HIS_DEATH_CAUSE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
