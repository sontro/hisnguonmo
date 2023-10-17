using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyCreate : EntityBase
    {
        public HisMediStockExtyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK_EXTY>();
        }

        private BridgeDAO<HIS_MEDI_STOCK_EXTY> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK_EXTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
