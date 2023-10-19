using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsRole
{
    partial class AcsRoleCheck : EntityBase
    {
        public AcsRoleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE>();
        }

        private BridgeDAO<ACS_ROLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
