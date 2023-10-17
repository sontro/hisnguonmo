using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDispenseType
{
    partial class HisDispenseTypeTruncate : EntityBase
    {
        public HisDispenseTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE_TYPE>();
        }

        private BridgeDAO<HIS_DISPENSE_TYPE> bridgeDAO;

        public bool Truncate(HIS_DISPENSE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DISPENSE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
