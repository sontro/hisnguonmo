using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeCheck : EntityBase
    {
        public HisMrCheckItemTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_ITEM_TYPE>();
        }

        private BridgeDAO<HIS_MR_CHECK_ITEM_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
