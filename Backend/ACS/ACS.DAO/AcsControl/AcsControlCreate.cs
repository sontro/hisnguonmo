using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsControl
{
    partial class AcsControlCreate : EntityBase
    {
        public AcsControlCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_CONTROL>();
        }

        private BridgeDAO<ACS_CONTROL> bridgeDAO;

        public bool Create(ACS_CONTROL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_CONTROL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
