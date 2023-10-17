using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockPeriod
{
    partial class HisMediStockPeriodUpdate : EntityBase
    {
        public HisMediStockPeriodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_PERIOD>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_PERIOD> bridgeDAO;

        public bool Update(HIS_MEDI_STOCK_PERIOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_STOCK_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
