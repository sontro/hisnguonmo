using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStockMaty
{
    partial class HisMediStockMatyCheck : EntityBase
    {
        public HisMediStockMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_MATY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
