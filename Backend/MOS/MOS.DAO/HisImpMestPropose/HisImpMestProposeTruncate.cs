using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestPropose
{
    partial class HisImpMestProposeTruncate : EntityBase
    {
        public HisImpMestProposeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PROPOSE>();
        }

        private BridgeDAO<HIS_IMP_MEST_PROPOSE> bridgeDAO;

        public bool Truncate(HIS_IMP_MEST_PROPOSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
