using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityCheck : EntityBase
    {
        public HisPeriodDriverDityCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERIOD_DRIVER_DITY>();
        }

        private BridgeDAO<HIS_PERIOD_DRIVER_DITY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
