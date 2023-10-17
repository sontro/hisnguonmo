using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeTruncate : EntityBase
    {
        public HisRationTimeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_TIME>();
        }

        private BridgeDAO<HIS_RATION_TIME> bridgeDAO;

        public bool Truncate(HIS_RATION_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_RATION_TIME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
