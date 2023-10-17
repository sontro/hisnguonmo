using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRefectory
{
    partial class HisRefectoryTruncate : EntityBase
    {
        public HisRefectoryTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REFECTORY>();
        }

        private BridgeDAO<HIS_REFECTORY> bridgeDAO;

        public bool Truncate(HIS_REFECTORY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REFECTORY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
