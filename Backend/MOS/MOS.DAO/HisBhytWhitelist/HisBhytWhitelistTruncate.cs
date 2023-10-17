using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytWhitelist
{
    partial class HisBhytWhitelistTruncate : EntityBase
    {
        public HisBhytWhitelistTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_WHITELIST>();
        }

        private BridgeDAO<HIS_BHYT_WHITELIST> bridgeDAO;

        public bool Truncate(HIS_BHYT_WHITELIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BHYT_WHITELIST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
