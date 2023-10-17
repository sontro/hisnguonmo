using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistCheck : EntityBase
    {
        public HisMrChecklistCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECKLIST>();
        }

        private BridgeDAO<HIS_MR_CHECKLIST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
