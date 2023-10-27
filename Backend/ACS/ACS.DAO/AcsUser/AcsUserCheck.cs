using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsUser
{
    partial class AcsUserCheck : EntityBase
    {
        public AcsUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_USER>();
        }

        private BridgeDAO<ACS_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
