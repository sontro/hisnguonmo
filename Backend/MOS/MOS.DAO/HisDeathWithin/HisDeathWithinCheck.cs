using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDeathWithin
{
    partial class HisDeathWithinCheck : EntityBase
    {
        public HisDeathWithinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_WITHIN>();
        }

        private BridgeDAO<HIS_DEATH_WITHIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
