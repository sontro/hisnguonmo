using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHeinApproval
{
    partial class HisHeinApprovalCheck : EntityBase
    {
        public HisHeinApprovalCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_APPROVAL>();
        }

        private BridgeDAO<HIS_HEIN_APPROVAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
