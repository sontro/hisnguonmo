using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsApplication
{
    partial class AcsApplicationUpdate : EntityBase
    {
        public AcsApplicationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION>();
        }

        private BridgeDAO<ACS_APPLICATION> bridgeDAO;

        public bool Update(ACS_APPLICATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_APPLICATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
