using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepositReason
{
    partial class HisDepositReasonTruncate : EntityBase
    {
        public HisDepositReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REASON>();
        }

        private BridgeDAO<HIS_DEPOSIT_REASON> bridgeDAO;

        public bool Truncate(HIS_DEPOSIT_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEPOSIT_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
