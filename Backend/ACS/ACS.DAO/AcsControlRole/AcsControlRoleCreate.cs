using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsControlRole
{
    partial class AcsControlRoleCreate : EntityBase
    {
        public AcsControlRoleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL_ROLE>();
        }

        private BridgeDAO<ACS_CONTROL_ROLE> bridgeDAO;

        public bool Create(ACS_CONTROL_ROLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_CONTROL_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
