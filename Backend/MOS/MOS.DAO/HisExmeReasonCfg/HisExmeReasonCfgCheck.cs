using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgCheck : EntityBase
    {
        public HisExmeReasonCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXME_REASON_CFG>();
        }

        private BridgeDAO<HIS_EXME_REASON_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
