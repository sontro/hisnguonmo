using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentType
{
    partial class HisTreatmentTypeCheck : EntityBase
    {
        public HisTreatmentTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
