using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentResult
{
    partial class HisTreatmentResultCreate : EntityBase
    {
        public HisTreatmentResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_RESULT>();
        }

        private BridgeDAO<HIS_TREATMENT_RESULT> bridgeDAO;

        public bool Create(HIS_TREATMENT_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
