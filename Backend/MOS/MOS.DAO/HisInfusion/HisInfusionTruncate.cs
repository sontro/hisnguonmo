using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusion
{
    partial class HisInfusionTruncate : EntityBase
    {
        public HisInfusionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION>();
        }

        private BridgeDAO<HIS_INFUSION> bridgeDAO;

        public bool Truncate(HIS_INFUSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INFUSION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
