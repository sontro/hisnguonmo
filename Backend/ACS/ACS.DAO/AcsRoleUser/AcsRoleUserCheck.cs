using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsRoleUser
{
    partial class AcsRoleUserCheck : EntityBase
    {
        public AcsRoleUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_USER>();
        }

        private BridgeDAO<ACS_ROLE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
