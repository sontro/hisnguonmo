using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBillFund
{
    partial class HisBillFundCheck : EntityBase
    {
        public HisBillFundCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_FUND>();
        }

        private BridgeDAO<HIS_BILL_FUND> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
