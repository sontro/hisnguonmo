using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodCheck : EntityBase
    {
        public HisMestPeriodBloodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLOOD>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLOOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
