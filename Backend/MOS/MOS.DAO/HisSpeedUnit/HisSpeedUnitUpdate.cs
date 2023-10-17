using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSpeedUnit
{
    partial class HisSpeedUnitUpdate : EntityBase
    {
        public HisSpeedUnitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPEED_UNIT>();
        }

        private BridgeDAO<HIS_SPEED_UNIT> bridgeDAO;

        public bool Update(HIS_SPEED_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SPEED_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
