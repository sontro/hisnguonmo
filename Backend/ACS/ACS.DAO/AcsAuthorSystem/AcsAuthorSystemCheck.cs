using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemCheck : EntityBase
    {
        public AcsAuthorSystemCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHOR_SYSTEM>();
        }

        private BridgeDAO<ACS_AUTHOR_SYSTEM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
