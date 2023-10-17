using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRejectAlert
{
    partial class HisRejectAlertTruncate : EntityBase
    {
        public HisRejectAlertTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REJECT_ALERT>();
        }

        private BridgeDAO<HIS_REJECT_ALERT> bridgeDAO;

        public bool Truncate(HIS_REJECT_ALERT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REJECT_ALERT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
