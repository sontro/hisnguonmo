using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUnlimitReason
{
    partial class HisUnlimitReasonUpdate : EntityBase
    {
        public HisUnlimitReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_REASON>();
        }

        private BridgeDAO<HIS_UNLIMIT_REASON> bridgeDAO;

        public bool Update(HIS_UNLIMIT_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_UNLIMIT_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
