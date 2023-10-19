using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleAuthor
{
    partial class AcsRoleAuthorUpdate : EntityBase
    {
        public AcsRoleAuthorUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_AUTHOR>();
        }

        private BridgeDAO<ACS_ROLE_AUTHOR> bridgeDAO;

        public bool Update(ACS_ROLE_AUTHOR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ROLE_AUTHOR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
