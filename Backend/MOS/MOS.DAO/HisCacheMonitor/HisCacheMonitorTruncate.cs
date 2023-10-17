using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCacheMonitor
{
    partial class HisCacheMonitorTruncate : EntityBase
    {
        public HisCacheMonitorTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CACHE_MONITOR>();
        }

        private BridgeDAO<HIS_CACHE_MONITOR> bridgeDAO;

        public bool Truncate(HIS_CACHE_MONITOR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CACHE_MONITOR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
