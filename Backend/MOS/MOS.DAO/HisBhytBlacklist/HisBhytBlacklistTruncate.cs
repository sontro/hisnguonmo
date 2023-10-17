using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytBlacklist
{
    partial class HisBhytBlacklistTruncate : EntityBase
    {
        public HisBhytBlacklistTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_BLACKLIST>();
        }

        private BridgeDAO<HIS_BHYT_BLACKLIST> bridgeDAO;

        public bool Truncate(HIS_BHYT_BLACKLIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BHYT_BLACKLIST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
