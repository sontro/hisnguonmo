using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediStock
{
    partial class HisMediStockCheck : EntityBase
    {
        public HisMediStockCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK>();
        }

        private BridgeDAO<HIS_MEDI_STOCK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
