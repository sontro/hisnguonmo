using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedLog
{
    partial class HisBedLogTruncate : EntityBase
    {
        public HisBedLogTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_LOG>();
        }

        private BridgeDAO<HIS_BED_LOG> bridgeDAO;

        public bool Truncate(HIS_BED_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BED_LOG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
