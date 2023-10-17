using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckSummary
{
    partial class HisMrCheckSummaryTruncate : EntityBase
    {
        public HisMrCheckSummaryTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_SUMMARY>();
        }

        private BridgeDAO<HIS_MR_CHECK_SUMMARY> bridgeDAO;

        public bool Truncate(HIS_MR_CHECK_SUMMARY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MR_CHECK_SUMMARY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
