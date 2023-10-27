using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestUpdate : EntityBase
    {
        public AcsAuthenRequestUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHEN_REQUEST>();
        }

        private BridgeDAO<ACS_AUTHEN_REQUEST> bridgeDAO;

        public bool Update(ACS_AUTHEN_REQUEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_AUTHEN_REQUEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
