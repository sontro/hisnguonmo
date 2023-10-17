using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsUpdate : EntityBase
    {
        public HisBillGoodsUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_GOODS>();
        }

        private BridgeDAO<HIS_BILL_GOODS> bridgeDAO;

        public bool Update(HIS_BILL_GOODS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BILL_GOODS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
