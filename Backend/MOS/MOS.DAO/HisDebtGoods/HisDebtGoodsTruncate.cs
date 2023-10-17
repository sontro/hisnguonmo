using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebtGoods
{
    partial class HisDebtGoodsTruncate : EntityBase
    {
        public HisDebtGoodsTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBT_GOODS>();
        }

        private BridgeDAO<HIS_DEBT_GOODS> bridgeDAO;

        public bool Truncate(HIS_DEBT_GOODS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBT_GOODS> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
