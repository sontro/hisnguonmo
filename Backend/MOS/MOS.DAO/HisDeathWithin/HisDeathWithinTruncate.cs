using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathWithin
{
    partial class HisDeathWithinTruncate : EntityBase
    {
        public HisDeathWithinTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_WITHIN>();
        }

        private BridgeDAO<HIS_DEATH_WITHIN> bridgeDAO;

        public bool Truncate(HIS_DEATH_WITHIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEATH_WITHIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
