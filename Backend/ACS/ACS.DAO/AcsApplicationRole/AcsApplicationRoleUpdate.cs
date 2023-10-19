using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsApplicationRole
{
    partial class AcsApplicationRoleUpdate : EntityBase
    {
        public AcsApplicationRoleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION_ROLE>();
        }

        private BridgeDAO<ACS_APPLICATION_ROLE> bridgeDAO;

        public bool Update(ACS_APPLICATION_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_APPLICATION_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
