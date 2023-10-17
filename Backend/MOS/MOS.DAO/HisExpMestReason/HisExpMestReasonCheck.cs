using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestReason
{
    partial class HisExpMestReasonCheck : EntityBase
    {
        public HisExpMestReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_REASON>();
        }

        private BridgeDAO<HIS_EXP_MEST_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
