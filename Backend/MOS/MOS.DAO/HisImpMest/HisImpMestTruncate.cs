using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMest
{
    partial class HisImpMestTruncate : EntityBase
    {
        public HisImpMestTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST>();
        }

        private BridgeDAO<HIS_IMP_MEST> bridgeDAO;

        public bool Truncate(HIS_IMP_MEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_MEST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
