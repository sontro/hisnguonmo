using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentGet : BusinessBase
    {
        internal HisMedicalAssessmentGet()
            : base()
        {

        }

        internal HisMedicalAssessmentGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICAL_ASSESSMENT> Get(HisMedicalAssessmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_ASSESSMENT GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicalAssessmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_ASSESSMENT GetById(long id, HisMedicalAssessmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.GetById(id, filter.Query());
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
