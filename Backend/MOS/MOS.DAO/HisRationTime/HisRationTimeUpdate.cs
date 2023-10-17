using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeUpdate : EntityBase
    {
        public HisRationTimeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_TIME>();
        }

        private BridgeDAO<HIS_RATION_TIME> bridgeDAO;

        public bool Update(HIS_RATION_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_RATION_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
