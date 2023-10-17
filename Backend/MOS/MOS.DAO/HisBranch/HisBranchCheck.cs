using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBranch
{
    partial class HisBranchCheck : EntityBase
    {
        public HisBranchCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH>();
        }

        private BridgeDAO<HIS_BRANCH> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
