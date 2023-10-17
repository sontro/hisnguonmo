using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckSummary
{
    partial class HisMrCheckSummaryUpdate : EntityBase
    {
        public HisMrCheckSummaryUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_SUMMARY>();
        }

        private BridgeDAO<HIS_MR_CHECK_SUMMARY> bridgeDAO;

        public bool Update(HIS_MR_CHECK_SUMMARY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MR_CHECK_SUMMARY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
