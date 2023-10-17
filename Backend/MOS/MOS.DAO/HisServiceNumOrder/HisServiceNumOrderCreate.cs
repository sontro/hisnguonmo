using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceNumOrder
{
    partial class HisServiceNumOrderCreate : EntityBase
    {
        public HisServiceNumOrderCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_NUM_ORDER>();
        }

        private BridgeDAO<HIS_SERVICE_NUM_ORDER> bridgeDAO;

        public bool Create(HIS_SERVICE_NUM_ORDER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_NUM_ORDER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
