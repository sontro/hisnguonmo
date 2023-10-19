using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsModuleRole
{
    partial class AcsModuleRoleCheck : EntityBase
    {
        public AcsModuleRoleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_ROLE>();
        }

        private BridgeDAO<ACS_MODULE_ROLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
