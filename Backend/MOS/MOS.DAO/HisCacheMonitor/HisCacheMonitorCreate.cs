using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCacheMonitor
{
    partial class HisCacheMonitorCreate : EntityBase
    {
        public HisCacheMonitorCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CACHE_MONITOR>();
        }

        private BridgeDAO<HIS_CACHE_MONITOR> bridgeDAO;

        public bool Create(HIS_CACHE_MONITOR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CACHE_MONITOR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
