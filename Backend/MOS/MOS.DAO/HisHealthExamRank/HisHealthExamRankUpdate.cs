using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHealthExamRank
{
    partial class HisHealthExamRankUpdate : EntityBase
    {
        public HisHealthExamRankUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEALTH_EXAM_RANK>();
        }

        private BridgeDAO<HIS_HEALTH_EXAM_RANK> bridgeDAO;

        public bool Update(HIS_HEALTH_EXAM_RANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HEALTH_EXAM_RANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
