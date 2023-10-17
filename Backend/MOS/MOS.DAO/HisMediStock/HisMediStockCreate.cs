using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStock
{
    partial class HisMediStockCreate : EntityBase
    {
        public HisMediStockCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_STOCK>();
        }

        private BridgeDAO<HIS_MEDI_STOCK> bridgeDAO;

        public bool Create(HIS_MEDI_STOCK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_STOCK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
