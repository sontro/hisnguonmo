using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsCredentialData
{
    partial class AcsCredentialDataCreate : EntityBase
    {
        public AcsCredentialDataCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CREDENTIAL_DATA>();
        }

        private BridgeDAO<ACS_CREDENTIAL_DATA> bridgeDAO;

        public bool Create(ACS_CREDENTIAL_DATA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_CREDENTIAL_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
