using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipPlan
{
    partial class HisEkipPlanUpdate : EntityBase
    {
        public HisEkipPlanUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN>();
        }

        private BridgeDAO<HIS_EKIP_PLAN> bridgeDAO;

        public bool Update(HIS_EKIP_PLAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EKIP_PLAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
