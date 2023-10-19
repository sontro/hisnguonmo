using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsRoleAuthor
{
    partial class AcsRoleAuthorCheck : EntityBase
    {
        public AcsRoleAuthorCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_AUTHOR>();
        }

        private BridgeDAO<ACS_ROLE_AUTHOR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
