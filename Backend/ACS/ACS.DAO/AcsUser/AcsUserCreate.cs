using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsUser
{
    partial class AcsUserCreate : EntityBase
    {
        public AcsUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_USER>();
        }

        private BridgeDAO<ACS_USER> bridgeDAO;

        public bool Create(ACS_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
