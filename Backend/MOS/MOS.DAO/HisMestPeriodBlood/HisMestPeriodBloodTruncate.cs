using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodTruncate : EntityBase
    {
        public HisMestPeriodBloodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLOOD>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLOOD> bridgeDAO;

        public bool Truncate(HIS_MEST_PERIOD_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
