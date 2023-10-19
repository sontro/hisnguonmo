using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModule
{
    partial class AcsModuleUpdate : EntityBase
    {
        public AcsModuleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE>();
        }

        private BridgeDAO<ACS_MODULE> bridgeDAO;

        public bool Update(ACS_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
