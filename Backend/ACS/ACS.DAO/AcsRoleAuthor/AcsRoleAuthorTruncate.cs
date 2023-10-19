using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleAuthor
{
    partial class AcsRoleAuthorTruncate : EntityBase
    {
        public AcsRoleAuthorTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_AUTHOR>();
        }

        private BridgeDAO<ACS_ROLE_AUTHOR> bridgeDAO;

        public bool Truncate(ACS_ROLE_AUTHOR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_ROLE_AUTHOR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
