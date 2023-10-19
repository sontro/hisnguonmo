using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsUser
{
    partial class AcsUserUpdate : EntityBase
    {
        public AcsUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_USER>();
        }

        private BridgeDAO<ACS_USER> bridgeDAO;

        public bool Update(ACS_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
