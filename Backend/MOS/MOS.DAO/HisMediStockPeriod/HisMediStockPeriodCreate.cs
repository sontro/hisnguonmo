using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockPeriod
{
    partial class HisMediStockPeriodCreate : EntityBase
    {
        public HisMediStockPeriodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_PERIOD>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_PERIOD> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK_PERIOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
