using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgTruncate : EntityBase
    {
        public HisExmeReasonCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXME_REASON_CFG>();
        }

        private BridgeDAO<HIS_EXME_REASON_CFG> bridgeDAO;

        public bool Truncate(HIS_EXME_REASON_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXME_REASON_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
