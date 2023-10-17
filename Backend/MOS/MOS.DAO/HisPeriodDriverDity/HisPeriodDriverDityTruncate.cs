using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityTruncate : EntityBase
    {
        public HisPeriodDriverDityTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERIOD_DRIVER_DITY>();
        }

        private BridgeDAO<HIS_PERIOD_DRIVER_DITY> bridgeDAO;

        public bool Truncate(HIS_PERIOD_DRIVER_DITY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
