using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebtGoods
{
    partial class HisDebtGoodsUpdate : EntityBase
    {
        public HisDebtGoodsUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBT_GOODS>();
        }

        private BridgeDAO<HIS_DEBT_GOODS> bridgeDAO;

        public bool Update(HIS_DEBT_GOODS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBT_GOODS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
