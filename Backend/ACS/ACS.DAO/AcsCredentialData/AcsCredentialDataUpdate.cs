using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsCredentialData
{
    partial class AcsCredentialDataUpdate : EntityBase
    {
        public AcsCredentialDataUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CREDENTIAL_DATA>();
        }

        private BridgeDAO<ACS_CREDENTIAL_DATA> bridgeDAO;

        public bool Update(ACS_CREDENTIAL_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_CREDENTIAL_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
