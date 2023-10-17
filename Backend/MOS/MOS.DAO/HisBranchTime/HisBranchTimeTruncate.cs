using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBranchTime
{
    partial class HisBranchTimeTruncate : EntityBase
    {
        public HisBranchTimeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH_TIME>();
        }

        private BridgeDAO<HIS_BRANCH_TIME> bridgeDAO;

        public bool Truncate(HIS_BRANCH_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BRANCH_TIME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
