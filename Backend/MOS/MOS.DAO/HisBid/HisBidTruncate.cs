using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBid
{
    partial class HisBidTruncate : EntityBase
    {
        public HisBidTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID>();
        }

        private BridgeDAO<HIS_BID> bridgeDAO;

        public bool Truncate(HIS_BID data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BID> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
