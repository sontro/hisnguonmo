using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmergencyWtime
{
    partial class HisEmergencyWtimeCheck : EntityBase
    {
        public HisEmergencyWtimeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMERGENCY_WTIME>();
        }

        private BridgeDAO<HIS_EMERGENCY_WTIME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
