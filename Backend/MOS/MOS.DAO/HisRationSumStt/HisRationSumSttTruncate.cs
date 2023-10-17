using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSumStt
{
    partial class HisRationSumSttTruncate : EntityBase
    {
        public HisRationSumSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM_STT>();
        }

        private BridgeDAO<HIS_RATION_SUM_STT> bridgeDAO;

        public bool Truncate(HIS_RATION_SUM_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_RATION_SUM_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
