using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestTruncate : EntityBase
    {
        public HisPtttGroupBestTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP_BEST>();
        }

        private BridgeDAO<HIS_PTTT_GROUP_BEST> bridgeDAO;

        public bool Truncate(HIS_PTTT_GROUP_BEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
