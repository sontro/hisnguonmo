using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStorageCondition
{
    partial class HisStorageConditionTruncate : EntityBase
    {
        public HisStorageConditionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STORAGE_CONDITION>();
        }

        private BridgeDAO<HIS_STORAGE_CONDITION> bridgeDAO;

        public bool Truncate(HIS_STORAGE_CONDITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_STORAGE_CONDITION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
