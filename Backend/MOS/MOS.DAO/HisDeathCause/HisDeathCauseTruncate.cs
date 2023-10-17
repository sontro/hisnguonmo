using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathCause
{
    partial class HisDeathCauseTruncate : EntityBase
    {
        public HisDeathCauseTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CAUSE>();
        }

        private BridgeDAO<HIS_DEATH_CAUSE> bridgeDAO;

        public bool Truncate(HIS_DEATH_CAUSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEATH_CAUSE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
