using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigUpdate : EntityBase
    {
        public HisEmrCoverConfigUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_CONFIG>();
        }

        private BridgeDAO<HIS_EMR_COVER_CONFIG> bridgeDAO;

        public bool Update(HIS_EMR_COVER_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
