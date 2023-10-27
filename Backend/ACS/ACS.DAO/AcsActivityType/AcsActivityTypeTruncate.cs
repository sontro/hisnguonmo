using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsActivityType
{
    partial class AcsActivityTypeTruncate : EntityBase
    {
        public AcsActivityTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_TYPE>();
        }

        private BridgeDAO<ACS_ACTIVITY_TYPE> bridgeDAO;

        public bool Truncate(ACS_ACTIVITY_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_ACTIVITY_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
