using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDbLog
{
    partial class HisDbLogTruncate : EntityBase
    {
        public HisDbLogTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DB_LOG>();
        }

        private BridgeDAO<HIS_DB_LOG> bridgeDAO;

        public bool Truncate(HIS_DB_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DB_LOG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
