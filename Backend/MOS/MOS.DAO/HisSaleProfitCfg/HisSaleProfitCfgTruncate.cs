using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgTruncate : EntityBase
    {
        public HisSaleProfitCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SALE_PROFIT_CFG>();
        }

        private BridgeDAO<HIS_SALE_PROFIT_CFG> bridgeDAO;

        public bool Truncate(HIS_SALE_PROFIT_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SALE_PROFIT_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
