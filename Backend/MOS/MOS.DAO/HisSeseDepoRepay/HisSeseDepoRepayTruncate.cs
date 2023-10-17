using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayTruncate : EntityBase
    {
        public HisSeseDepoRepayTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_DEPO_REPAY>();
        }

        private BridgeDAO<HIS_SESE_DEPO_REPAY> bridgeDAO;

        public bool Truncate(HIS_SESE_DEPO_REPAY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
