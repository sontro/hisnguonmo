using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAlert
{
    partial class HisAlertTruncate : EntityBase
    {
        public HisAlertTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALERT>();
        }

        private BridgeDAO<HIS_ALERT> bridgeDAO;

        public bool Truncate(HIS_ALERT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ALERT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
