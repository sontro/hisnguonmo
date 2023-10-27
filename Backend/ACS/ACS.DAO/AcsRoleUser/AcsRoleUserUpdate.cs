using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleUser
{
    partial class AcsRoleUserUpdate : EntityBase
    {
        public AcsRoleUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_USER>();
        }

        private BridgeDAO<ACS_ROLE_USER> bridgeDAO;

        public bool Update(ACS_ROLE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ROLE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
