using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipPlanUser
{
    partial class HisEkipPlanUserCreate : EntityBase
    {
        public HisEkipPlanUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN_USER>();
        }

        private BridgeDAO<HIS_EKIP_PLAN_USER> bridgeDAO;

        public bool Create(HIS_EKIP_PLAN_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EKIP_PLAN_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
