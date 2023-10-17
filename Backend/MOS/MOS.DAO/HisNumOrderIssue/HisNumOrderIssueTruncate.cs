using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderIssue
{
    partial class HisNumOrderIssueTruncate : EntityBase
    {
        public HisNumOrderIssueTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_ISSUE>();
        }

        private BridgeDAO<HIS_NUM_ORDER_ISSUE> bridgeDAO;

        public bool Truncate(HIS_NUM_ORDER_ISSUE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
