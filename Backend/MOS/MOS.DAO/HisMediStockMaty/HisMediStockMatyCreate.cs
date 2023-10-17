using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockMaty
{
    partial class HisMediStockMatyCreate : EntityBase
    {
        public HisMediStockMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_MATY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_MATY> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
