using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestCheck : EntityBase
    {
        public AcsAuthenRequestCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHEN_REQUEST>();
        }

        private BridgeDAO<ACS_AUTHEN_REQUEST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
