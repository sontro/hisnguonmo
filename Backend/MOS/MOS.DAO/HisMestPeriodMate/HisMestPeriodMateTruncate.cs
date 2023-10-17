using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMate
{
    partial class HisMestPeriodMateTruncate : EntityBase
    {
        public HisMestPeriodMateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATE>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATE> bridgeDAO;

        public bool Truncate(HIS_MEST_PERIOD_MATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PERIOD_MATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
