using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverCheck : EntityBase
    {
        public HisKskPeriodDriverCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_PERIOD_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_PERIOD_DRIVER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
