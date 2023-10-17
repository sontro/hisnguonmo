using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockImty
{
    partial class HisMediStockImtyTruncate : EntityBase
    {
        public HisMediStockImtyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_IMTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_IMTY> bridgeDAO;

        public bool Truncate(HIS_MEDI_STOCK_IMTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
