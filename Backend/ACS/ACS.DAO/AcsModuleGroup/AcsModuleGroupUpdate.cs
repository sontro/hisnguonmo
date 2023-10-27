using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModuleGroup
{
    partial class AcsModuleGroupUpdate : EntityBase
    {
        public AcsModuleGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_GROUP>();
        }

        private BridgeDAO<ACS_MODULE_GROUP> bridgeDAO;

        public bool Update(ACS_MODULE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_MODULE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
