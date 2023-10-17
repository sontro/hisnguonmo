using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisConfig
{
    partial class HisConfigTruncate : EntityBase
    {
        public HisConfigTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG>();
        }

        private BridgeDAO<HIS_CONFIG> bridgeDAO;

        public bool Truncate(HIS_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CONFIG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
