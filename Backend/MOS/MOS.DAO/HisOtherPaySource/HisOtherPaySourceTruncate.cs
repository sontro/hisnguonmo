using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisOtherPaySource
{
    partial class HisOtherPaySourceTruncate : EntityBase
    {
        public HisOtherPaySourceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OTHER_PAY_SOURCE>();
        }

        private BridgeDAO<HIS_OTHER_PAY_SOURCE> bridgeDAO;

        public bool Truncate(HIS_OTHER_PAY_SOURCE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_OTHER_PAY_SOURCE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
