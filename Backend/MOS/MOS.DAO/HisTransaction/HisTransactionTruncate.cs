using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransaction
{
    partial class HisTransactionTruncate : EntityBase
    {
        public HisTransactionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION>();
        }

        private BridgeDAO<HIS_TRANSACTION> bridgeDAO;

        public bool Truncate(HIS_TRANSACTION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANSACTION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
