using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyUpdate : EntityBase
    {
        public HisMediStockExtyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_EXTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_EXTY> bridgeDAO;

        public bool Update(HIS_MEDI_STOCK_EXTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
