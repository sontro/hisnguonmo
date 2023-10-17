using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleCheck : EntityBase
    {
        public HisRationScheduleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SCHEDULE>();
        }

        private BridgeDAO<HIS_RATION_SCHEDULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
