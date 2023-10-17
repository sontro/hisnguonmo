using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTrackingTemp
{
    partial class HisTrackingTempTruncate : EntityBase
    {
        public HisTrackingTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING_TEMP>();
        }

        private BridgeDAO<HIS_TRACKING_TEMP> bridgeDAO;

        public bool Truncate(HIS_TRACKING_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRACKING_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
