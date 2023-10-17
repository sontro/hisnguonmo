using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyCheck : EntityBase
    {
        public HisMediStockExtyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_EXTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_EXTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
