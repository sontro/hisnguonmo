using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMilitaryRank
{
    partial class HisMilitaryRankUpdate : EntityBase
    {
        public HisMilitaryRankUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MILITARY_RANK>();
        }

        private BridgeDAO<HIS_MILITARY_RANK> bridgeDAO;

        public bool Update(HIS_MILITARY_RANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MILITARY_RANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
