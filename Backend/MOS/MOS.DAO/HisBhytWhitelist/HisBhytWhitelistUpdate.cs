using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytWhitelist
{
    partial class HisBhytWhitelistUpdate : EntityBase
    {
        public HisBhytWhitelistUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_WHITELIST>();
        }

        private BridgeDAO<HIS_BHYT_WHITELIST> bridgeDAO;

        public bool Update(HIS_BHYT_WHITELIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BHYT_WHITELIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
