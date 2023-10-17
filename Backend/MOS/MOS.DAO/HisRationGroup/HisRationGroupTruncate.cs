using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationGroup
{
    partial class HisRationGroupTruncate : EntityBase
    {
        public HisRationGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_GROUP>();
        }

        private BridgeDAO<HIS_RATION_GROUP> bridgeDAO;

        public bool Truncate(HIS_RATION_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_RATION_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
