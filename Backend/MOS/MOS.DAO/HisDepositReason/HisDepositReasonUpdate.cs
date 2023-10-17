using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepositReason
{
    partial class HisDepositReasonUpdate : EntityBase
    {
        public HisDepositReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REASON>();
        }

        private BridgeDAO<HIS_DEPOSIT_REASON> bridgeDAO;

        public bool Update(HIS_DEPOSIT_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEPOSIT_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
