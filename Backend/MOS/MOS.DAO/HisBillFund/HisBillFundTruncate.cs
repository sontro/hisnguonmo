using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillFund
{
    partial class HisBillFundTruncate : EntityBase
    {
        public HisBillFundTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BILL_FUND>();
        }

        private BridgeDAO<HIS_BILL_FUND> bridgeDAO;

        public bool Truncate(HIS_BILL_FUND data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BILL_FUND> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
