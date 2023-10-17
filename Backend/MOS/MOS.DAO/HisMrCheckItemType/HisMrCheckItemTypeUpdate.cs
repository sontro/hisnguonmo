using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeUpdate : EntityBase
    {
        public HisMrCheckItemTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM_TYPE>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM_TYPE> bridgeDAO;

        public bool Update(HIS_MR_CHECK_ITEM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
