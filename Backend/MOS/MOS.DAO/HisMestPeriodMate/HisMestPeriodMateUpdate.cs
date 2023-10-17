using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMate
{
    partial class HisMestPeriodMateUpdate : EntityBase
    {
        public HisMestPeriodMateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATE>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATE> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_MATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_MATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
