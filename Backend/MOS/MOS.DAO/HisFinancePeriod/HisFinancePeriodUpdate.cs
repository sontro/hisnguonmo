using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFinancePeriod
{
    partial class HisFinancePeriodUpdate : EntityBase
    {
        public HisFinancePeriodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FINANCE_PERIOD>();
        }

        private BridgeDAO<HIS_FINANCE_PERIOD> bridgeDAO;

        public bool Update(HIS_FINANCE_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FINANCE_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
