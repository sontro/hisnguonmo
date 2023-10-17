using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisNumOrderIssue
{
    partial class HisNumOrderIssueCheck : EntityBase
    {
        public HisNumOrderIssueCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_ISSUE>();
        }

        private BridgeDAO<HIS_NUM_ORDER_ISSUE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
