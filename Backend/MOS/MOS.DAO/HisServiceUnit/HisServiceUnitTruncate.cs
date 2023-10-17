using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceUnit
{
    partial class HisServiceUnitTruncate : EntityBase
    {
        public HisServiceUnitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_UNIT>();
        }

        private BridgeDAO<HIS_SERVICE_UNIT> bridgeDAO;

        public bool Truncate(HIS_SERVICE_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_UNIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
