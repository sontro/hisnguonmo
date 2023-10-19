using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemUpdate : EntityBase
    {
        public AcsAuthorSystemUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHOR_SYSTEM>();
        }

        private BridgeDAO<ACS_AUTHOR_SYSTEM> bridgeDAO;

        public bool Update(ACS_AUTHOR_SYSTEM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_AUTHOR_SYSTEM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
