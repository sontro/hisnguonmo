using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMety
{
    partial class HisMestPeriodMetyCheck : EntityBase
    {
        public HisMestPeriodMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_METY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
