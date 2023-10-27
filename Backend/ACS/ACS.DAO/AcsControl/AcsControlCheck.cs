using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsControl
{
    partial class AcsControlCheck : EntityBase
    {
        public AcsControlCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL>();
        }

        private BridgeDAO<ACS_CONTROL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
