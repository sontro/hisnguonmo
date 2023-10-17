using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleCheck : EntityBase
    {
        public HisEmployeeScheduleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE_SCHEDULE>();
        }

        private BridgeDAO<HIS_EMPLOYEE_SCHEDULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
