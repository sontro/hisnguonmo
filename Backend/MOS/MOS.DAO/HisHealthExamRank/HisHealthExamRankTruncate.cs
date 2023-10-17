using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHealthExamRank
{
    partial class HisHealthExamRankTruncate : EntityBase
    {
        public HisHealthExamRankTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEALTH_EXAM_RANK>();
        }

        private BridgeDAO<HIS_HEALTH_EXAM_RANK> bridgeDAO;

        public bool Truncate(HIS_HEALTH_EXAM_RANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HEALTH_EXAM_RANK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
