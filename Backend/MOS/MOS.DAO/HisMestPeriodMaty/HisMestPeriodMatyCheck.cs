using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMaty
{
    partial class HisMestPeriodMatyCheck : EntityBase
    {
        public HisMestPeriodMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
