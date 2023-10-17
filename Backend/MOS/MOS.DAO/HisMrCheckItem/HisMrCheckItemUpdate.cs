using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemUpdate : EntityBase
    {
        public HisMrCheckItemUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM> bridgeDAO;

        public bool Update(HIS_MR_CHECK_ITEM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MR_CHECK_ITEM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
