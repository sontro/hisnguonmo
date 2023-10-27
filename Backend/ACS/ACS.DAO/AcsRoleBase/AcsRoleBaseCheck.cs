using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsRoleBase
{
    partial class AcsRoleBaseCheck : EntityBase
    {
        public AcsRoleBaseCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_BASE>();
        }

        private BridgeDAO<ACS_ROLE_BASE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
