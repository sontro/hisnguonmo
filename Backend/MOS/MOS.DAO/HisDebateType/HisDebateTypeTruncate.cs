using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateType
{
    partial class HisDebateTypeTruncate : EntityBase
    {
        public HisDebateTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TYPE>();
        }

        private BridgeDAO<HIS_DEBATE_TYPE> bridgeDAO;

        public bool Truncate(HIS_DEBATE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
