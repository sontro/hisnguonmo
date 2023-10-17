using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipPlan
{
    partial class HisEkipPlanTruncate : EntityBase
    {
        public HisEkipPlanTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN>();
        }

        private BridgeDAO<HIS_EKIP_PLAN> bridgeDAO;

        public bool Truncate(HIS_EKIP_PLAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EKIP_PLAN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
