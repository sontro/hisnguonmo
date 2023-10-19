using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsToken
{
    partial class AcsTokenCreate : EntityBase
    {
        public AcsTokenCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_TOKEN>();
        }

        private BridgeDAO<ACS_TOKEN> bridgeDAO;

        public bool Create(ACS_TOKEN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_TOKEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
