using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMedi
{
    partial class HisMestPeriodMediTruncate : EntityBase
    {
        public HisMestPeriodMediTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MEDI>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MEDI> bridgeDAO;

        public bool Truncate(HIS_MEST_PERIOD_MEDI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
