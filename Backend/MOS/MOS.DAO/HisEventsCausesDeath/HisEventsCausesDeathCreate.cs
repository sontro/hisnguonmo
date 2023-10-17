using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathCreate : EntityBase
    {
        public HisEventsCausesDeathCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EVENTS_CAUSES_DEATH>();
        }

        private BridgeDAO<HIS_EVENTS_CAUSES_DEATH> bridgeDAO;

        public bool Create(HIS_EVENTS_CAUSES_DEATH data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
