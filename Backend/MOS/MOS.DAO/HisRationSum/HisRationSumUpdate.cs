using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSum
{
    partial class HisRationSumUpdate : EntityBase
    {
        public HisRationSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_SUM>();
        }

        private BridgeDAO<HIS_RATION_SUM> bridgeDAO;

        public bool Update(HIS_RATION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_RATION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
