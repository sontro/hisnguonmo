using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockMety
{
    partial class HisMediStockMetyUpdate : EntityBase
    {
        public HisMediStockMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_METY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_METY> bridgeDAO;

        public bool Update(HIS_MEDI_STOCK_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_STOCK_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
