using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExamSchedule
{
    partial class HisExamScheduleCheck : EntityBase
    {
        public HisExamScheduleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SCHEDULE>();
        }

        private BridgeDAO<HIS_EXAM_SCHEDULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
