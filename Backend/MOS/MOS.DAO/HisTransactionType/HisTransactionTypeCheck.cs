using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransactionType
{
    partial class HisTransactionTypeCheck : EntityBase
    {
        public HisTransactionTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_TYPE>();
        }

        private BridgeDAO<HIS_TRANSACTION_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
