using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgTruncate : EntityBase
    {
        public HisExpiredDateCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXPIRED_DATE_CFG>();
        }

        private BridgeDAO<HIS_EXPIRED_DATE_CFG> bridgeDAO;

        public bool Truncate(HIS_EXPIRED_DATE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
