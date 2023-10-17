using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationGroup
{
    partial class HisRationGroupUpdate : EntityBase
    {
        public HisRationGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_GROUP>();
        }

        private BridgeDAO<HIS_RATION_GROUP> bridgeDAO;

        public bool Update(HIS_RATION_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_RATION_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
