using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisArea
{
    partial class HisAreaTruncate : EntityBase
    {
        public HisAreaTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AREA>();
        }

        private BridgeDAO<HIS_AREA> bridgeDAO;

        public bool Truncate(HIS_AREA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_AREA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
