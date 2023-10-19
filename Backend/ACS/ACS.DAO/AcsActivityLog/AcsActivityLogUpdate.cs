using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsActivityLog
{
    partial class AcsActivityLogUpdate : EntityBase
    {
        public AcsActivityLogUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_LOG>();
        }

        private BridgeDAO<ACS_ACTIVITY_LOG> bridgeDAO;

        public bool Update(ACS_ACTIVITY_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ACTIVITY_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
