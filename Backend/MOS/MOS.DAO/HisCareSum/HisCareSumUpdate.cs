using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareSum
{
    partial class HisCareSumUpdate : EntityBase
    {
        public HisCareSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_SUM>();
        }

        private BridgeDAO<HIS_CARE_SUM> bridgeDAO;

        public bool Update(HIS_CARE_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARE_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
