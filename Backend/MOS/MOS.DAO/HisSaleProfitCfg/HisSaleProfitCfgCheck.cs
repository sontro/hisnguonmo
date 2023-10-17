using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgCheck : EntityBase
    {
        public HisSaleProfitCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SALE_PROFIT_CFG>();
        }

        private BridgeDAO<HIS_SALE_PROFIT_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
