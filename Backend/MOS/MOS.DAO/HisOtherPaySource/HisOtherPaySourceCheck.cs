using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisOtherPaySource
{
    partial class HisOtherPaySourceCheck : EntityBase
    {
        public HisOtherPaySourceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OTHER_PAY_SOURCE>();
        }

        private BridgeDAO<HIS_OTHER_PAY_SOURCE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
