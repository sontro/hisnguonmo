using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytBlacklist
{
    partial class HisBhytBlacklistUpdate : EntityBase
    {
        public HisBhytBlacklistUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_BLACKLIST>();
        }

        private BridgeDAO<HIS_BHYT_BLACKLIST> bridgeDAO;

        public bool Update(HIS_BHYT_BLACKLIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BHYT_BLACKLIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
