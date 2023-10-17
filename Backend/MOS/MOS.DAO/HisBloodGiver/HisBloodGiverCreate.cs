using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodGiver
{
    partial class HisBloodGiverCreate : EntityBase
    {
        public HisBloodGiverCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GIVER>();
        }

        private BridgeDAO<HIS_BLOOD_GIVER> bridgeDAO;

        public bool Create(HIS_BLOOD_GIVER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BLOOD_GIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
