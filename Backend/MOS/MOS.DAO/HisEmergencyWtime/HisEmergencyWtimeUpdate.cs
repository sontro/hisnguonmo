using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmergencyWtime
{
    partial class HisEmergencyWtimeUpdate : EntityBase
    {
        public HisEmergencyWtimeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMERGENCY_WTIME>();
        }

        private BridgeDAO<HIS_EMERGENCY_WTIME> bridgeDAO;

        public bool Update(HIS_EMERGENCY_WTIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMERGENCY_WTIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
