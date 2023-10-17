using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisConfigGroup
{
    partial class HisConfigGroupTruncate : EntityBase
    {
        public HisConfigGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG_GROUP>();
        }

        private BridgeDAO<HIS_CONFIG_GROUP> bridgeDAO;

        public bool Truncate(HIS_CONFIG_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CONFIG_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
