using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsActivityLog
{
    partial class AcsActivityLogTruncate : EntityBase
    {
        public AcsActivityLogTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_LOG>();
        }

        private BridgeDAO<ACS_ACTIVITY_LOG> bridgeDAO;

        public bool Truncate(ACS_ACTIVITY_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_ACTIVITY_LOG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
