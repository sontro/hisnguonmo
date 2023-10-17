using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPaanLiquid
{
    partial class HisPaanLiquidTruncate : EntityBase
    {
        public HisPaanLiquidTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_LIQUID>();
        }

        private BridgeDAO<HIS_PAAN_LIQUID> bridgeDAO;

        public bool Truncate(HIS_PAAN_LIQUID data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PAAN_LIQUID> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
