using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitCheck : EntityBase
    {
        public HisTreatmentUnlimitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_UNLIMIT>();
        }

        private BridgeDAO<HIS_TREATMENT_UNLIMIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
