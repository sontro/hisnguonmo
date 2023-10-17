using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoldReturn
{
    partial class HisHoldReturnTruncate : EntityBase
    {
        public HisHoldReturnTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOLD_RETURN>();
        }

        private BridgeDAO<HIS_HOLD_RETURN> bridgeDAO;

        public bool Truncate(HIS_HOLD_RETURN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HOLD_RETURN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
