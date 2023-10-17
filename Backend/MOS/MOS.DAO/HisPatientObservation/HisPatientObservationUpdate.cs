using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationUpdate : EntityBase
    {
        public HisPatientObservationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_OBSERVATION>();
        }

        private BridgeDAO<HIS_PATIENT_OBSERVATION> bridgeDAO;

        public bool Update(HIS_PATIENT_OBSERVATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
