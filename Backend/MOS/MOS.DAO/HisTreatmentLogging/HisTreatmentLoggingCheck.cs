using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentLogging
{
    partial class HisTreatmentLoggingCheck : EntityBase
    {
        public HisTreatmentLoggingCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOGGING>();
        }

        private BridgeDAO<HIS_TREATMENT_LOGGING> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
