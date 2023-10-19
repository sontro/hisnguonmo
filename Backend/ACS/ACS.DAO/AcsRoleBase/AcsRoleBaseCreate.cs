using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleBase
{
    partial class AcsRoleBaseCreate : EntityBase
    {
        public AcsRoleBaseCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_BASE>();
        }

        private BridgeDAO<ACS_ROLE_BASE> bridgeDAO;

        public bool Create(ACS_ROLE_BASE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ROLE_BASE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
