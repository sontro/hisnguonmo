using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipPlan
{
    partial class HisEkipPlanCreate : EntityBase
    {
        public HisEkipPlanCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_PLAN>();
        }

        private BridgeDAO<HIS_EKIP_PLAN> bridgeDAO;

        public bool Create(HIS_EKIP_PLAN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EKIP_PLAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
