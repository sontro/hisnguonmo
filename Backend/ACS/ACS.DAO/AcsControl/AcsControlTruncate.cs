using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsControl
{
    partial class AcsControlTruncate : EntityBase
    {
        public AcsControlTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL>();
        }

        private BridgeDAO<ACS_CONTROL> bridgeDAO;

        public bool Truncate(ACS_CONTROL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_CONTROL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
