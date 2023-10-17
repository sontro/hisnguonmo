using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTrackingTemp
{
    partial class HisTrackingTempUpdate : EntityBase
    {
        public HisTrackingTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING_TEMP>();
        }

        private BridgeDAO<HIS_TRACKING_TEMP> bridgeDAO;

        public bool Update(HIS_TRACKING_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRACKING_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
