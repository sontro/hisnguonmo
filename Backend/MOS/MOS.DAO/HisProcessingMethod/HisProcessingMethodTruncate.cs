using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisProcessingMethod
{
    partial class HisProcessingMethodTruncate : EntityBase
    {
        public HisProcessingMethodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROCESSING_METHOD>();
        }

        private BridgeDAO<HIS_PROCESSING_METHOD> bridgeDAO;

        public bool Truncate(HIS_PROCESSING_METHOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PROCESSING_METHOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
