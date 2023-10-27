using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemTruncate : EntityBase
    {
        public AcsAuthorSystemTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHOR_SYSTEM>();
        }

        private BridgeDAO<ACS_AUTHOR_SYSTEM> bridgeDAO;

        public bool Truncate(ACS_AUTHOR_SYSTEM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_AUTHOR_SYSTEM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
