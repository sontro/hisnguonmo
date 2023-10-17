using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockPeriod
{
    partial class HisMediStockPeriodTruncate : EntityBase
    {
        public HisMediStockPeriodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_PERIOD>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_PERIOD> bridgeDAO;

        public bool Truncate(HIS_MEDI_STOCK_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_STOCK_PERIOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
