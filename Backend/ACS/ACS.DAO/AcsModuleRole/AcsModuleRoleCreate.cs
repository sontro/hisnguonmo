using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleRole
{
    partial class AcsModuleRoleCreate : EntityBase
    {
        public AcsModuleRoleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_ROLE>();
        }

        private BridgeDAO<ACS_MODULE_ROLE> bridgeDAO;

        public bool Create(ACS_MODULE_ROLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_MODULE_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
