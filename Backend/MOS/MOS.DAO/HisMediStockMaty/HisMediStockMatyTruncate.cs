using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockMaty
{
    partial class HisMediStockMatyTruncate : EntityBase
    {
        public HisMediStockMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_MATY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_MATY> bridgeDAO;

        public bool Truncate(HIS_MEDI_STOCK_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_STOCK_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
