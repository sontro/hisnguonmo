using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleUser
{
    partial class AcsRoleUserCreate : EntityBase
    {
        public AcsRoleUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_USER>();
        }

        private BridgeDAO<ACS_ROLE_USER> bridgeDAO;

        public bool Create(ACS_ROLE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ROLE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
