using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMate
{
    partial class HisMestPeriodMateCheck : EntityBase
    {
        public HisMestPeriodMateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATE>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
