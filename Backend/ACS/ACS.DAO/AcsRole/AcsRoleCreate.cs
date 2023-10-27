using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsRole
{
    partial class AcsRoleCreate : EntityBase
    {
        public AcsRoleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE>();
        }

        private BridgeDAO<ACS_ROLE> bridgeDAO;

        public bool Create(ACS_ROLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
