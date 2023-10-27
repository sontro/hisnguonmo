using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleBase
{
    partial class AcsRoleBaseUpdate : EntityBase
    {
        public AcsRoleBaseUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_BASE>();
        }

        private BridgeDAO<ACS_ROLE_BASE> bridgeDAO;

        public bool Update(ACS_ROLE_BASE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ROLE_BASE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
