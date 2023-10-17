using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientTypeSub
{
    partial class HisPatientTypeSubCheck : EntityBase
    {
        public HisPatientTypeSubCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_SUB>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_SUB> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
