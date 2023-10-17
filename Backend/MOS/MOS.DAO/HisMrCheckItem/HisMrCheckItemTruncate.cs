using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemTruncate : EntityBase
    {
        public HisMrCheckItemTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM> bridgeDAO;

        public bool Truncate(HIS_MR_CHECK_ITEM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MR_CHECK_ITEM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
