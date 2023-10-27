using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsApplicationRole
{
    partial class AcsApplicationRoleCheck : EntityBase
    {
        public AcsApplicationRoleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION_ROLE>();
        }

        private BridgeDAO<ACS_APPLICATION_ROLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
