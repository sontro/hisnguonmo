using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleUpdate : EntityBase
    {
        public HisRationScheduleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SCHEDULE>();
        }

        private BridgeDAO<HIS_RATION_SCHEDULE> bridgeDAO;

        public bool Update(HIS_RATION_SCHEDULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_RATION_SCHEDULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
