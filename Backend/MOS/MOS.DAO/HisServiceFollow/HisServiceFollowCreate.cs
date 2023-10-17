using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceFollow
{
    partial class HisServiceFollowCreate : EntityBase
    {
        public HisServiceFollowCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_FOLLOW>();
        }

        private BridgeDAO<HIS_SERVICE_FOLLOW> bridgeDAO;

        public bool Create(HIS_SERVICE_FOLLOW data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_FOLLOW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
