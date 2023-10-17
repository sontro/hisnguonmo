using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgCheck : EntityBase
    {
        public HisExpiredDateCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXPIRED_DATE_CFG>();
        }

        private BridgeDAO<HIS_EXPIRED_DATE_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
