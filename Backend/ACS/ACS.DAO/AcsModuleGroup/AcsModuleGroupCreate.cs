using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleGroup
{
    partial class AcsModuleGroupCreate : EntityBase
    {
        public AcsModuleGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE_GROUP>();
        }

        private BridgeDAO<ACS_MODULE_GROUP> bridgeDAO;

        public bool Create(ACS_MODULE_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_MODULE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
