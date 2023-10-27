using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsControlRole
{
    partial class AcsControlRoleCheck : EntityBase
    {
        public AcsControlRoleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL_ROLE>();
        }

        private BridgeDAO<ACS_CONTROL_ROLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
