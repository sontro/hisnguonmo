using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornPosition
{
    partial class HisBornPositionTruncate : EntityBase
    {
        public HisBornPositionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_POSITION>();
        }

        private BridgeDAO<HIS_BORN_POSITION> bridgeDAO;

        public bool Truncate(HIS_BORN_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BORN_POSITION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
