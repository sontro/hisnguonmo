using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebtGoods
{
    partial class HisDebtGoodsCheck : EntityBase
    {
        public HisDebtGoodsCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBT_GOODS>();
        }

        private BridgeDAO<HIS_DEBT_GOODS> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
