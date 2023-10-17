using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentLogType
{
    partial class HisTreatmentLogTypeCheck : EntityBase
    {
        public HisTreatmentLogTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOG_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_LOG_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
