using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMety
{
    partial class HisMestPeriodMetyUpdate : EntityBase
    {
        public HisMestPeriodMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_METY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_METY> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
