using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsApplicationRole
{
    partial class AcsApplicationRoleCreate : EntityBase
    {
        public AcsApplicationRoleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION_ROLE>();
        }

        private BridgeDAO<ACS_APPLICATION_ROLE> bridgeDAO;

        public bool Create(ACS_APPLICATION_ROLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_APPLICATION_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
