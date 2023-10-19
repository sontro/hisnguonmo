using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsActivityLog
{
    partial class AcsActivityLogCheck : EntityBase
    {
        public AcsActivityLogCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_LOG>();
        }

        private BridgeDAO<ACS_ACTIVITY_LOG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
