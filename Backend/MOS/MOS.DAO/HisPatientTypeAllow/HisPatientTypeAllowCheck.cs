using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAllow
{
    partial class HisPatientTypeAllowCheck : EntityBase
    {
        public HisPatientTypeAllowCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALLOW>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALLOW> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
