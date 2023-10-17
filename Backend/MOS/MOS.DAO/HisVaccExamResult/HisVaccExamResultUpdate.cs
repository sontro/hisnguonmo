using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccExamResult
{
    partial class HisVaccExamResultUpdate : EntityBase
    {
        public HisVaccExamResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_EXAM_RESULT>();
        }

        private BridgeDAO<HIS_VACC_EXAM_RESULT> bridgeDAO;

        public bool Update(HIS_VACC_EXAM_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
