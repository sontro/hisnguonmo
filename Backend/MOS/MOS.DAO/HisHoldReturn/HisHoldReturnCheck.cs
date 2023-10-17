using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHoldReturn
{
    partial class HisHoldReturnCheck : EntityBase
    {
        public HisHoldReturnCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOLD_RETURN>();
        }

        private BridgeDAO<HIS_HOLD_RETURN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
