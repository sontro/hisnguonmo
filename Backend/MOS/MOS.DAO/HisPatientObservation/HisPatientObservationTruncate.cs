using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationTruncate : EntityBase
    {
        public HisPatientObservationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_OBSERVATION>();
        }

        private BridgeDAO<HIS_PATIENT_OBSERVATION> bridgeDAO;

        public bool Truncate(HIS_PATIENT_OBSERVATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
