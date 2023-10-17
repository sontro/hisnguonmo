using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsCheck : EntityBase
    {
        public HisBillGoodsCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_GOODS>();
        }

        private BridgeDAO<HIS_BILL_GOODS> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
