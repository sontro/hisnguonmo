using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceGroup
{
    partial class HisServiceGroupCreate : EntityBase
    {
        public HisServiceGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_GROUP>();
        }

        private BridgeDAO<HIS_SERVICE_GROUP> bridgeDAO;

        public bool Create(HIS_SERVICE_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
