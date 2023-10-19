using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleAuthor
{
    partial class AcsRoleAuthorCreate : EntityBase
    {
        public AcsRoleAuthorCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE_AUTHOR>();
        }

        private BridgeDAO<ACS_ROLE_AUTHOR> bridgeDAO;

        public bool Create(ACS_ROLE_AUTHOR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ROLE_AUTHOR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
