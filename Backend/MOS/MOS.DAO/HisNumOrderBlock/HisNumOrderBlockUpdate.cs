using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderBlock
{
    partial class HisNumOrderBlockUpdate : EntityBase
    {
        public HisNumOrderBlockUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_BLOCK>();
        }

        private BridgeDAO<HIS_NUM_ORDER_BLOCK> bridgeDAO;

        public bool Update(HIS_NUM_ORDER_BLOCK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
