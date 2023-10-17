using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUnlimitReason
{
    partial class HisUnlimitReasonCheck : EntityBase
    {
        public HisUnlimitReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_REASON>();
        }

        private BridgeDAO<HIS_UNLIMIT_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
