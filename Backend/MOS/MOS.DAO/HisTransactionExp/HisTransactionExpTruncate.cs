using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpTruncate : EntityBase
    {
        public HisTransactionExpTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_EXP>();
        }

        private BridgeDAO<HIS_TRANSACTION_EXP> bridgeDAO;

        public bool Truncate(HIS_TRANSACTION_EXP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANSACTION_EXP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
