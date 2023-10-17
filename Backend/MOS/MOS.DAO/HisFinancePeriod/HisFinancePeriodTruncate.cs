using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFinancePeriod
{
    partial class HisFinancePeriodTruncate : EntityBase
    {
        public HisFinancePeriodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FINANCE_PERIOD>();
        }

        private BridgeDAO<HIS_FINANCE_PERIOD> bridgeDAO;

        public bool Truncate(HIS_FINANCE_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FINANCE_PERIOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
