using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgTruncate : EntityBase
    {
        public HisWarningFeeCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WARNING_FEE_CFG>();
        }

        private BridgeDAO<HIS_WARNING_FEE_CFG> bridgeDAO;

        public bool Truncate(HIS_WARNING_FEE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_WARNING_FEE_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
