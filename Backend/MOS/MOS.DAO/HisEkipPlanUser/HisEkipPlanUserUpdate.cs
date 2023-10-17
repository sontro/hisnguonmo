using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipPlanUser
{
    partial class HisEkipPlanUserUpdate : EntityBase
    {
        public HisEkipPlanUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN_USER>();
        }

        private BridgeDAO<HIS_EKIP_PLAN_USER> bridgeDAO;

        public bool Update(HIS_EKIP_PLAN_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EKIP_PLAN_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
