using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverUpdate : EntityBase
    {
        public HisKskPeriodDriverUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_PERIOD_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_PERIOD_DRIVER> bridgeDAO;

        public bool Update(HIS_KSK_PERIOD_DRIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
