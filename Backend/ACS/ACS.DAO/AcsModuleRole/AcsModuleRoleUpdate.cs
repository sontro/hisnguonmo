using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModuleRole
{
    partial class AcsModuleRoleUpdate : EntityBase
    {
        public AcsModuleRoleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_ROLE>();
        }

        private BridgeDAO<ACS_MODULE_ROLE> bridgeDAO;

        public bool Update(ACS_MODULE_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_MODULE_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
