using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCancelReason
{
    partial class HisCancelReasonCheck : EntityBase
    {
        public HisCancelReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CANCEL_REASON>();
        }

        private BridgeDAO<HIS_CANCEL_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
