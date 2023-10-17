using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStockPeriod
{
    partial class HisMediStockPeriodCheck : EntityBase
    {
        public HisMediStockPeriodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_PERIOD>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_PERIOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
