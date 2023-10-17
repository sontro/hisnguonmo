using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashout
{
    partial class HisCashoutTruncate : EntityBase
    {
        public HisCashoutTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHOUT>();
        }

        private BridgeDAO<HIS_CASHOUT> bridgeDAO;

        public bool Truncate(HIS_CASHOUT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CASHOUT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
