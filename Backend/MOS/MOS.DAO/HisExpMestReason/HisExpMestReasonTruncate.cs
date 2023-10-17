using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestReason
{
    partial class HisExpMestReasonTruncate : EntityBase
    {
        public HisExpMestReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_REASON>();
        }

        private BridgeDAO<HIS_EXP_MEST_REASON> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
