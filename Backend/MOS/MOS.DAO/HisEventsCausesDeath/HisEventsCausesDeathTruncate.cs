using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathTruncate : EntityBase
    {
        public HisEventsCausesDeathTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EVENTS_CAUSES_DEATH>();
        }

        private BridgeDAO<HIS_EVENTS_CAUSES_DEATH> bridgeDAO;

        public bool Truncate(HIS_EVENTS_CAUSES_DEATH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
