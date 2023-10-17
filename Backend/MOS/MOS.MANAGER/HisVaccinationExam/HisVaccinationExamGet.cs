using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamGet : BusinessBase
    {
        internal HisVaccinationExamGet()
            : base()
        {

        }

        internal HisVaccinationExamGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_EXAM> Get(HisVaccinationExamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationExamDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_EXAM GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationExamFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_EXAM GetById(long id, HisVaccinationExamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationExamDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION_EXAM> GetByPatientId(long patientId)
        {
            try
            {
                HisVaccinationExamFilterQuery filter = new HisVaccinationExamFilterQuery();
                filter.PATIENT_ID = patientId;
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
