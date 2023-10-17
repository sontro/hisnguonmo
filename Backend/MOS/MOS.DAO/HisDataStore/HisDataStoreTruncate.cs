using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDataStore
{
    partial class HisDataStoreTruncate : EntityBase
    {
        public HisDataStoreTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DATA_STORE>();
        }

        private BridgeDAO<HIS_DATA_STORE> bridgeDAO;

        public bool Truncate(HIS_DATA_STORE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DATA_STORE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
