using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsControlRole
{
    partial class AcsControlRoleTruncate : EntityBase
    {
        public AcsControlRoleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL_ROLE>();
        }

        private BridgeDAO<ACS_CONTROL_ROLE> bridgeDAO;

        public bool Truncate(ACS_CONTROL_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_CONTROL_ROLE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
