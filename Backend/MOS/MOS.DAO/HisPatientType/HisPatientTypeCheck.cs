using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientType
{
    partial class HisPatientTypeCheck : EntityBase
    {
        public HisPatientTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
