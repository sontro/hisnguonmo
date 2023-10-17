using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamSchedule
{
    partial class HisExamScheduleTruncate : EntityBase
    {
        public HisExamScheduleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SCHEDULE>();
        }

        private BridgeDAO<HIS_EXAM_SCHEDULE> bridgeDAO;

        public bool Truncate(HIS_EXAM_SCHEDULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXAM_SCHEDULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
