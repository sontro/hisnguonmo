using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModuleRole
{
    partial class AcsModuleRoleTruncate : EntityBase
    {
        public AcsModuleRoleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_ROLE>();
        }

        private BridgeDAO<ACS_MODULE_ROLE> bridgeDAO;

        public bool Truncate(ACS_MODULE_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_MODULE_ROLE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
