using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    partial class HisPatientObservationGet : BusinessBase
    {
        internal HisPatientObservationGet()
            : base()
        {

        }

        internal HisPatientObservationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PATIENT_OBSERVATION> Get(HisPatientObservationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientObservationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_OBSERVATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisPatientObservationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_OBSERVATION GetById(long id, HisPatientObservationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientObservationDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PATIENT_OBSERVATION> GetByTreatmentBedRoomId(long treatmentBedRoomId)
        {
            try
            {
                HisPatientObservationFilterQuery filter = new HisPatientObservationFilterQuery();
                filter.TREATMENT_BED_ROOM_ID = treatmentBedRoomId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
