using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccExamResult
{
    partial class HisVaccExamResultCheck : EntityBase
    {
        public HisVaccExamResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_EXAM_RESULT>();
        }

        private BridgeDAO<HIS_VACC_EXAM_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
