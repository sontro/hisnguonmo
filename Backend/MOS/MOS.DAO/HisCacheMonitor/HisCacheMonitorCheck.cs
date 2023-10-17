using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCacheMonitor
{
    partial class HisCacheMonitorCheck : EntityBase
    {
        public HisCacheMonitorCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CACHE_MONITOR>();
        }

        private BridgeDAO<HIS_CACHE_MONITOR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
