using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMaty
{
    partial class HisMestPeriodMatyUpdate : EntityBase
    {
        public HisMestPeriodMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATY> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
