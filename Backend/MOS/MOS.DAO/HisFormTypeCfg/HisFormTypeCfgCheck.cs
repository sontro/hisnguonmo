using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfg
{
    partial class HisFormTypeCfgCheck : EntityBase
    {
        public HisFormTypeCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
