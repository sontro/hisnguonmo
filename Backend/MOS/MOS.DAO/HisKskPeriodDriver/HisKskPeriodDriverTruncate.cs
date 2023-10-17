using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverTruncate : EntityBase
    {
        public HisKskPeriodDriverTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_PERIOD_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_PERIOD_DRIVER> bridgeDAO;

        public bool Truncate(HIS_KSK_PERIOD_DRIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
