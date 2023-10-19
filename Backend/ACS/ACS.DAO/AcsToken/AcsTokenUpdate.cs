using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsToken
{
    partial class AcsTokenUpdate : EntityBase
    {
        public AcsTokenUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_TOKEN>();
        }

        private BridgeDAO<ACS_TOKEN> bridgeDAO;

        public bool Update(ACS_TOKEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_TOKEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
