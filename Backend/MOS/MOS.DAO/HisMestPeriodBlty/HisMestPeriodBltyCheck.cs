using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyCheck : EntityBase
    {
        public HisMestPeriodBltyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLTY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
