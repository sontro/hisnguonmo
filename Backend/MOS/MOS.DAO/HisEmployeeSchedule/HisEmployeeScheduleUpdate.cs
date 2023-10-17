using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleUpdate : EntityBase
    {
        public HisEmployeeScheduleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE_SCHEDULE>();
        }

        private BridgeDAO<HIS_EMPLOYEE_SCHEDULE> bridgeDAO;

        public bool Update(HIS_EMPLOYEE_SCHEDULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
