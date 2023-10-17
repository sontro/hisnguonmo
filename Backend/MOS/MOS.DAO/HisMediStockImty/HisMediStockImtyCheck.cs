using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStockImty
{
    partial class HisMediStockImtyCheck : EntityBase
    {
        public HisMediStockImtyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_IMTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_IMTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
