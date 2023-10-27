using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsModuleGroup
{
    partial class AcsModuleGroupCheck : EntityBase
    {
        public AcsModuleGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_GROUP>();
        }

        private BridgeDAO<ACS_MODULE_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
