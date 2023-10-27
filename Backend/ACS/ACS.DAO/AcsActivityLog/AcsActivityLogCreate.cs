using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace ACS.DAO.AcsActivityLog
{
    partial class AcsActivityLogCreate : EntityBase
    {
        public AcsActivityLogCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ACTIVITY_LOG>();
        }

        private BridgeDAO<ACS_ACTIVITY_LOG> bridgeDAO;

        public bool Create(ACS_ACTIVITY_LOG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<ACS_ACTIVITY_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
