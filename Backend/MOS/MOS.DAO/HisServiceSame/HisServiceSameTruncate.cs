using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceSame
{
    partial class HisServiceSameTruncate : EntityBase
    {
        public HisServiceSameTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_SAME>();
        }

        private BridgeDAO<HIS_SERVICE_SAME> bridgeDAO;

        public bool Truncate(HIS_SERVICE_SAME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_SAME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
