using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmergencyWtime
{
    partial class HisEmergencyWtimeTruncate : EntityBase
    {
        public HisEmergencyWtimeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMERGENCY_WTIME>();
        }

        private BridgeDAO<HIS_EMERGENCY_WTIME> bridgeDAO;

        public bool Truncate(HIS_EMERGENCY_WTIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMERGENCY_WTIME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
