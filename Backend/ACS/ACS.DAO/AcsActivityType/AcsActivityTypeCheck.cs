using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsActivityType
{
    partial class AcsActivityTypeCheck : EntityBase
    {
        public AcsActivityTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_TYPE>();
        }

        private BridgeDAO<ACS_ACTIVITY_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
