using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigTruncate : EntityBase
    {
        public HisEmrCoverConfigTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_CONFIG>();
        }

        private BridgeDAO<HIS_EMR_COVER_CONFIG> bridgeDAO;

        public bool Truncate(HIS_EMR_COVER_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
