using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemCheck : EntityBase
    {
        public HisMrCheckItemCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
