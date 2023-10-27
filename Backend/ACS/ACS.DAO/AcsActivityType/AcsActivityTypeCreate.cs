using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsActivityType
{
    partial class AcsActivityTypeCreate : EntityBase
    {
        public AcsActivityTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_TYPE>();
        }

        private BridgeDAO<ACS_ACTIVITY_TYPE> bridgeDAO;

        public bool Create(ACS_ACTIVITY_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ACTIVITY_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
