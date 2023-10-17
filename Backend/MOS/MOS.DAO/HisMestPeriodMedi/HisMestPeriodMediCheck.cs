using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMedi
{
    partial class HisMestPeriodMediCheck : EntityBase
    {
        public HisMestPeriodMediCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MEDI>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MEDI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
