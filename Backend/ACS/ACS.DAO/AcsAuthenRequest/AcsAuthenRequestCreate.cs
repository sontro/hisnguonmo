using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestCreate : EntityBase
    {
        public AcsAuthenRequestCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHEN_REQUEST>();
        }

        private BridgeDAO<ACS_AUTHEN_REQUEST> bridgeDAO;

        public bool Create(ACS_AUTHEN_REQUEST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_AUTHEN_REQUEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
