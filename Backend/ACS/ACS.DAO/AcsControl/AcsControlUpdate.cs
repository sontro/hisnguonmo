using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsControl
{
    partial class AcsControlUpdate : EntityBase
    {
        public AcsControlUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL>();
        }

        private BridgeDAO<ACS_CONTROL> bridgeDAO;

        public bool Update(ACS_CONTROL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_CONTROL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
