using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsApplication
{
    partial class AcsApplicationCreate : EntityBase
    {
        public AcsApplicationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_APPLICATION>();
        }

        private BridgeDAO<ACS_APPLICATION> bridgeDAO;

        public bool Create(ACS_APPLICATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_APPLICATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
