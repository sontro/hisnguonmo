using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsToken
{
    partial class AcsTokenCheck : EntityBase
    {
        public AcsTokenCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_TOKEN>();
        }

        private BridgeDAO<ACS_TOKEN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
