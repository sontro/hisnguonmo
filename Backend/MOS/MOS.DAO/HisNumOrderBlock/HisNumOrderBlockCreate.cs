using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderBlock
{
    partial class HisNumOrderBlockCreate : EntityBase
    {
        public HisNumOrderBlockCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_BLOCK>();
        }

        private BridgeDAO<HIS_NUM_ORDER_BLOCK> bridgeDAO;

        public bool Create(HIS_NUM_ORDER_BLOCK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
