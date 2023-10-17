using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRepayReason
{
    partial class HisRepayReasonTruncate : EntityBase
    {
        public HisRepayReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPAY_REASON>();
        }

        private BridgeDAO<HIS_REPAY_REASON> bridgeDAO;

        public bool Truncate(HIS_REPAY_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REPAY_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
