using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleTruncate : EntityBase
    {
        public HisEmployeeScheduleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE_SCHEDULE>();
        }

        private BridgeDAO<HIS_EMPLOYEE_SCHEDULE> bridgeDAO;

        public bool Truncate(HIS_EMPLOYEE_SCHEDULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
