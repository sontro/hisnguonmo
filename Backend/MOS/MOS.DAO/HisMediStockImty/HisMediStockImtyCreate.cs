using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockImty
{
    partial class HisMediStockImtyCreate : EntityBase
    {
        public HisMediStockImtyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_IMTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_IMTY> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK_IMTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
