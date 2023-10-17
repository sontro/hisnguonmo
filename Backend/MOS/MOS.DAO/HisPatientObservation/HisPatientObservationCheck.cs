using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationCheck : EntityBase
    {
        public HisPatientObservationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_OBSERVATION>();
        }

        private BridgeDAO<HIS_PATIENT_OBSERVATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
