using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodUpdate : EntityBase
    {
        public HisMestPeriodBloodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLOOD>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLOOD> bridgeDAO;

        public bool Update(HIS_MEST_PERIOD_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
