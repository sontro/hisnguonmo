using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMilitaryRank
{
    partial class HisMilitaryRankTruncate : EntityBase
    {
        public HisMilitaryRankTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MILITARY_RANK>();
        }

        private BridgeDAO<HIS_MILITARY_RANK> bridgeDAO;

        public bool Truncate(HIS_MILITARY_RANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MILITARY_RANK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
