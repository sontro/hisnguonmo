using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMest
{
    partial class HisExpMestTruncate : EntityBase
    {
        public HisExpMestTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST>();
        }

        private BridgeDAO<HIS_EXP_MEST> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
