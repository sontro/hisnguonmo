using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsTruncate : EntityBase
    {
        public HisBillGoodsTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_GOODS>();
        }

        private BridgeDAO<HIS_BILL_GOODS> bridgeDAO;

        public bool Truncate(HIS_BILL_GOODS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BILL_GOODS> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
