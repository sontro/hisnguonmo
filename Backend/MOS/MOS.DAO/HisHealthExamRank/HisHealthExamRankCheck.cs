using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHealthExamRank
{
    partial class HisHealthExamRankCheck : EntityBase
    {
        public HisHealthExamRankCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEALTH_EXAM_RANK>();
        }

        private BridgeDAO<HIS_HEALTH_EXAM_RANK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
