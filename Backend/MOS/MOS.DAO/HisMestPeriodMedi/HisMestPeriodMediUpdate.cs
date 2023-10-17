using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMedi
{
    partial class HisMestPeriodMediUpdate : EntityBase
    {
        public HisMestPeriodMediUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MEDI>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MEDI> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_MEDI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
