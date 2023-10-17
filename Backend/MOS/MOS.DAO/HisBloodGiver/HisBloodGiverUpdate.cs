using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodGiver
{
    partial class HisBloodGiverUpdate : EntityBase
    {
        public HisBloodGiverUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GIVER>();
        }

        private BridgeDAO<HIS_BLOOD_GIVER> bridgeDAO;

        public bool Update(HIS_BLOOD_GIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD_GIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
