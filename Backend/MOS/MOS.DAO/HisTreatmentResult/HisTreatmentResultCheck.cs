using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentResult
{
    partial class HisTreatmentResultCheck : EntityBase
    {
        public HisTreatmentResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_RESULT>();
        }

        private BridgeDAO<HIS_TREATMENT_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
