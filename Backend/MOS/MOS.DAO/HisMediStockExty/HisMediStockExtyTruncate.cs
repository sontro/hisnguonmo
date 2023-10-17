using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyTruncate : EntityBase
    {
        public HisMediStockExtyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_EXTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_EXTY> bridgeDAO;

        public bool Truncate(HIS_MEDI_STOCK_EXTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
