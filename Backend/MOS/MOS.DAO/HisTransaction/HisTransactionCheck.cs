using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTransaction
{
    partial class HisTransactionCheck : EntityBase
    {
        public HisTransactionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION>();
        }

        private BridgeDAO<HIS_TRANSACTION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
