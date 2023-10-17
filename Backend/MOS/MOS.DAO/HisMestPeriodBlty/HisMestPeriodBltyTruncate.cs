using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyTruncate : EntityBase
    {
        public HisMestPeriodBltyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLTY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLTY> bridgeDAO;

        public bool Truncate(HIS_MEST_PERIOD_BLTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
