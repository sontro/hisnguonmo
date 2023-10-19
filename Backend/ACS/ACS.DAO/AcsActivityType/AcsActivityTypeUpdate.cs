using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsActivityType
{
    partial class AcsActivityTypeUpdate : EntityBase
    {
        public AcsActivityTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_TYPE>();
        }

        private BridgeDAO<ACS_ACTIVITY_TYPE> bridgeDAO;

        public bool Update(ACS_ACTIVITY_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ACTIVITY_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
