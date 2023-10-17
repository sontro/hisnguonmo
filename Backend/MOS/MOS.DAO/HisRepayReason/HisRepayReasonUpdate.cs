using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRepayReason
{
    partial class HisRepayReasonUpdate : EntityBase
    {
        public HisRepayReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPAY_REASON>();
        }

        private BridgeDAO<HIS_REPAY_REASON> bridgeDAO;

        public bool Update(HIS_REPAY_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REPAY_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
