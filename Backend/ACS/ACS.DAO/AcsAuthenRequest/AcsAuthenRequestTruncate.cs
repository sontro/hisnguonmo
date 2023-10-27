using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestTruncate : EntityBase
    {
        public AcsAuthenRequestTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHEN_REQUEST>();
        }

        private BridgeDAO<ACS_AUTHEN_REQUEST> bridgeDAO;

        public bool Truncate(ACS_AUTHEN_REQUEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_AUTHEN_REQUEST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
