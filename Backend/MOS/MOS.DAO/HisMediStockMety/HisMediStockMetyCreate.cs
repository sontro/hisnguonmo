using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockMety
{
    partial class HisMediStockMetyCreate : EntityBase
    {
        public HisMediStockMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_METY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_METY> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
