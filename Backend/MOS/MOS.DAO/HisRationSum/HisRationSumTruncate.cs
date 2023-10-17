using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSum
{
    partial class HisRationSumTruncate : EntityBase
    {
        public HisRationSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM>();
        }

        private BridgeDAO<HIS_RATION_SUM> bridgeDAO;

        public bool Truncate(HIS_RATION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_RATION_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
