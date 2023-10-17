using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccExamResult
{
    partial class HisVaccExamResultCreate : EntityBase
    {
        public HisVaccExamResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_EXAM_RESULT>();
        }

        private BridgeDAO<HIS_VACC_EXAM_RESULT> bridgeDAO;

        public bool Create(HIS_VACC_EXAM_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
