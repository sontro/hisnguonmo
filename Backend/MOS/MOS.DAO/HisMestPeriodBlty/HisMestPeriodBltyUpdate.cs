using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyUpdate : EntityBase
    {
        public HisMestPeriodBltyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLTY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLTY> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_BLTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
