using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBhytWhitelist
{
    partial class HisBhytWhitelistCheck : EntityBase
    {
        public HisBhytWhitelistCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_WHITELIST>();
        }

        private BridgeDAO<HIS_BHYT_WHITELIST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
