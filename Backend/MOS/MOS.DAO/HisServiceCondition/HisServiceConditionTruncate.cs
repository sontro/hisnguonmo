using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceCondition
{
    partial class HisServiceConditionTruncate : EntityBase
    {
        public HisServiceConditionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_CONDITION>();
        }

        private BridgeDAO<HIS_SERVICE_CONDITION> bridgeDAO;

        public bool Truncate(HIS_SERVICE_CONDITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_CONDITION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
