using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsCreate : EntityBase
    {
        public HisBillGoodsCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_GOODS>();
        }

        private BridgeDAO<HIS_BILL_GOODS> bridgeDAO;

        public bool Create(HIS_BILL_GOODS data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BILL_GOODS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
