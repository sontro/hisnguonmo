using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionType
{
    partial class HisTransactionTypeTruncate : EntityBase
    {
        public HisTransactionTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_TYPE>();
        }

        private BridgeDAO<HIS_TRANSACTION_TYPE> bridgeDAO;

        public bool Truncate(HIS_TRANSACTION_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANSACTION_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
