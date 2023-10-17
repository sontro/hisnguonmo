using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSpeedUnit
{
    partial class HisSpeedUnitTruncate : EntityBase
    {
        public HisSpeedUnitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPEED_UNIT>();
        }

        private BridgeDAO<HIS_SPEED_UNIT> bridgeDAO;

        public bool Truncate(HIS_SPEED_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SPEED_UNIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
