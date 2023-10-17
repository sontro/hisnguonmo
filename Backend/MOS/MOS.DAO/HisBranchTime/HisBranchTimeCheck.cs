using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBranchTime
{
    partial class HisBranchTimeCheck : EntityBase
    {
        public HisBranchTimeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH_TIME>();
        }

        private BridgeDAO<HIS_BRANCH_TIME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
