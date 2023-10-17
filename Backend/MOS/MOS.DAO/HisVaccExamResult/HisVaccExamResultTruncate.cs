using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccExamResult
{
    partial class HisVaccExamResultTruncate : EntityBase
    {
        public HisVaccExamResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_EXAM_RESULT>();
        }

        private BridgeDAO<HIS_VACC_EXAM_RESULT> bridgeDAO;

        public bool Truncate(HIS_VACC_EXAM_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
