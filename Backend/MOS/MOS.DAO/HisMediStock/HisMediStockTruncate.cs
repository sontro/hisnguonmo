using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStock
{
    partial class HisMediStockTruncate : EntityBase
    {
        public HisMediStockTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK>();
        }

        private BridgeDAO<HIS_MEDI_STOCK> bridgeDAO;

        public bool Truncate(HIS_MEDI_STOCK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_STOCK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
