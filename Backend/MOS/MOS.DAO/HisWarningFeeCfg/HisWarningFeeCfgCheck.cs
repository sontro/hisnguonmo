using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgCheck : EntityBase
    {
        public HisWarningFeeCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WARNING_FEE_CFG>();
        }

        private BridgeDAO<HIS_WARNING_FEE_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
