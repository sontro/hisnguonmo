using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpCheck : EntityBase
    {
        public HisTransactionExpCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_EXP>();
        }

        private BridgeDAO<HIS_TRANSACTION_EXP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
