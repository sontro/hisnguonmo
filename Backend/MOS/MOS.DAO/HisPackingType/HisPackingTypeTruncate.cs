using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPackingType
{
    partial class HisPackingTypeTruncate : EntityBase
    {
        public HisPackingTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKING_TYPE>();
        }

        private BridgeDAO<HIS_PACKING_TYPE> bridgeDAO;

        public bool Truncate(HIS_PACKING_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PACKING_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
