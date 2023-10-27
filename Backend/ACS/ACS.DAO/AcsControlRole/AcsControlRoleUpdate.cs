using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsControlRole
{
    partial class AcsControlRoleUpdate : EntityBase
    {
        public AcsControlRoleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL_ROLE>();
        }

        private BridgeDAO<ACS_CONTROL_ROLE> bridgeDAO;

        public bool Update(ACS_CONTROL_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_CONTROL_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
