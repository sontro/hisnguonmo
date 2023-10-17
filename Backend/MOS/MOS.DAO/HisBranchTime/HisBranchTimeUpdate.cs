using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBranchTime
{
    partial class HisBranchTimeUpdate : EntityBase
    {
        public HisBranchTimeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH_TIME>();
        }

        private BridgeDAO<HIS_BRANCH_TIME> bridgeDAO;

        public bool Update(HIS_BRANCH_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BRANCH_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
