using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.DAO.AcsCredentialData
{
    partial class AcsCredentialDataCheck : EntityBase
    {
        public AcsCredentialDataCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CREDENTIAL_DATA>();
        }

        private BridgeDAO<ACS_CREDENTIAL_DATA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
