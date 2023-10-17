using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumTruncate : EntityBase
    {
        public HisInfusionSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION_SUM>();
        }

        private BridgeDAO<HIS_INFUSION_SUM> bridgeDAO;

        public bool Truncate(HIS_INFUSION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INFUSION_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
