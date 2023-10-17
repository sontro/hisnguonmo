using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPosition
{
    partial class HisPositionTruncate : EntityBase
    {
        public HisPositionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_POSITION>();
        }

        private BridgeDAO<HIS_POSITION> bridgeDAO;

        public bool Truncate(HIS_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_POSITION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
