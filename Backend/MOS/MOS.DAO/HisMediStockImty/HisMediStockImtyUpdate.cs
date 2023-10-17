using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockImty
{
    partial class HisMediStockImtyUpdate : EntityBase
    {
        public HisMediStockImtyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_IMTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_IMTY> bridgeDAO;

        public bool Update(HIS_MEDI_STOCK_IMTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
