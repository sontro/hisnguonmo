using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsApplication
{
    partial class AcsApplicationCheck : EntityBase
    {
        public AcsApplicationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION>();
        }

        private BridgeDAO<ACS_APPLICATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
