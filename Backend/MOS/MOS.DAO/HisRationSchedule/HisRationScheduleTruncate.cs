using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleTruncate : EntityBase
    {
        public HisRationScheduleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SCHEDULE>();
        }

        private BridgeDAO<HIS_RATION_SCHEDULE> bridgeDAO;

        public bool Truncate(HIS_RATION_SCHEDULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_RATION_SCHEDULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
