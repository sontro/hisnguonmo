using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderBlock
{
    partial class HisNumOrderBlockTruncate : EntityBase
    {
        public HisNumOrderBlockTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_BLOCK>();
        }

        private BridgeDAO<HIS_NUM_ORDER_BLOCK> bridgeDAO;

        public bool Truncate(HIS_NUM_ORDER_BLOCK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
