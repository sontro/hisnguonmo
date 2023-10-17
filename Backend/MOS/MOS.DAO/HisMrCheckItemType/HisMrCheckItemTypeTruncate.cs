using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeTruncate : EntityBase
    {
        public HisMrCheckItemTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM_TYPE>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM_TYPE> bridgeDAO;

        public bool Truncate(HIS_MR_CHECK_ITEM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
