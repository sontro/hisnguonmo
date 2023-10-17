using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDispense
{
    partial class HisDispenseTruncate : EntityBase
    {
        public HisDispenseTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE>();
        }

        private BridgeDAO<HIS_DISPENSE> bridgeDAO;

        public bool Truncate(HIS_DISPENSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DISPENSE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
