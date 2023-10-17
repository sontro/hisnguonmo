using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateTemp
{
    partial class HisDebateTempTruncate : EntityBase
    {
        public HisDebateTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TEMP>();
        }

        private BridgeDAO<HIS_DEBATE_TEMP> bridgeDAO;

        public bool Truncate(HIS_DEBATE_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
