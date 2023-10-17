using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTracking
{
    partial class HisTrackingTruncate : EntityBase
    {
        public HisTrackingTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING>();
        }

        private BridgeDAO<HIS_TRACKING> bridgeDAO;

        public bool Truncate(HIS_TRACKING data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRACKING> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
