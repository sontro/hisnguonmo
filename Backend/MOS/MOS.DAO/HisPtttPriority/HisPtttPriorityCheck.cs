using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttPriority
{
    partial class HisPtttPriorityCheck : EntityBase
    {
        public HisPtttPriorityCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_PRIORITY>();
        }

        private BridgeDAO<HIS_PTTT_PRIORITY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
