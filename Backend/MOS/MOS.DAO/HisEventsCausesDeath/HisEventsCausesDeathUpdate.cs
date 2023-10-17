using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathUpdate : EntityBase
    {
        public HisEventsCausesDeathUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EVENTS_CAUSES_DEATH>();
        }

        private BridgeDAO<HIS_EVENTS_CAUSES_DEATH> bridgeDAO;

        public bool Update(HIS_EVENTS_CAUSES_DEATH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
