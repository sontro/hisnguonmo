using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemCreate : EntityBase
    {
        public AcsAuthorSystemCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_AUTHOR_SYSTEM>();
        }

        private BridgeDAO<ACS_AUTHOR_SYSTEM> bridgeDAO;

        public bool Create(ACS_AUTHOR_SYSTEM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_AUTHOR_SYSTEM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
