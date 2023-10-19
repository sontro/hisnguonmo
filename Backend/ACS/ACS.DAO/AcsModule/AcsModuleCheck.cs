using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsModule
{
    partial class AcsModuleCheck : EntityBase
    {
        public AcsModuleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE>();
        }

        private BridgeDAO<ACS_MODULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
