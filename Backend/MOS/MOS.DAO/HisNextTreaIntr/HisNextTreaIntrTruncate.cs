using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrTruncate : EntityBase
    {
        public HisNextTreaIntrTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NEXT_TREA_INTR>();
        }

        private BridgeDAO<HIS_NEXT_TREA_INTR> bridgeDAO;

        public bool Truncate(HIS_NEXT_TREA_INTR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_NEXT_TREA_INTR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
