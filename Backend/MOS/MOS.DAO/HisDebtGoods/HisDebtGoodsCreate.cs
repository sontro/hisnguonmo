using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebtGoods
{
    partial class HisDebtGoodsCreate : EntityBase
    {
        public HisDebtGoodsCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBT_GOODS>();
        }

        private BridgeDAO<HIS_DEBT_GOODS> bridgeDAO;

        public bool Create(HIS_DEBT_GOODS data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBT_GOODS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
