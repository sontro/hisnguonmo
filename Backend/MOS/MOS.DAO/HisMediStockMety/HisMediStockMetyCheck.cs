using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStockMety
{
    partial class HisMediStockMetyCheck : EntityBase
    {
        public HisMediStockMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_METY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
