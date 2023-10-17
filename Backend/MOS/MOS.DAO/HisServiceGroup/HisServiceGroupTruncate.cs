using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceGroup
{
    partial class HisServiceGroupTruncate : EntityBase
    {
        public HisServiceGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_GROUP>();
        }

        private BridgeDAO<HIS_SERVICE_GROUP> bridgeDAO;

        public bool Truncate(HIS_SERVICE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
