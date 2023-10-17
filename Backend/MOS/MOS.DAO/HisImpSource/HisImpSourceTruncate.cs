using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpSource
{
    partial class HisImpSourceTruncate : EntityBase
    {
        public HisImpSourceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_SOURCE>();
        }

        private BridgeDAO<HIS_IMP_SOURCE> bridgeDAO;

        public bool Truncate(HIS_IMP_SOURCE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_SOURCE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
