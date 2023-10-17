using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisConfig
{
    partial class HisConfigUpdate : EntityBase
    {
        public HisConfigUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG>();
        }

        private BridgeDAO<HIS_CONFIG> bridgeDAO;

        public bool Update(HIS_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
