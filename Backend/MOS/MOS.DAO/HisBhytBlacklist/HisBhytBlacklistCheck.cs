using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBhytBlacklist
{
    partial class HisBhytBlacklistCheck : EntityBase
    {
        public HisBhytBlacklistCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_BLACKLIST>();
        }

        private BridgeDAO<HIS_BHYT_BLACKLIST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
