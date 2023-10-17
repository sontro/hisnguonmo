using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceHein
{
    partial class HisServiceHeinTruncate : EntityBase
    {
        public HisServiceHeinTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_HEIN>();
        }

        private BridgeDAO<HIS_SERVICE_HEIN> bridgeDAO;

        public bool Truncate(HIS_SERVICE_HEIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_HEIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
