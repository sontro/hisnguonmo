using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBranch
{
    partial class HisBranchTruncate : EntityBase
    {
        public HisBranchTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH>();
        }

        private BridgeDAO<HIS_BRANCH> bridgeDAO;

        public bool Truncate(HIS_BRANCH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BRANCH> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
