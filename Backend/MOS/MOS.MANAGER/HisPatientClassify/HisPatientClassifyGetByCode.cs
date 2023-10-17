using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientClassify
{
    partial class HisPatientClassifyGet : BusinessBase
    {
        internal HIS_PATIENT_CLASSIFY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPatientClassifyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_CLASSIFY GetByCode(string code, HisPatientClassifyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientClassifyDAO.GetByCode(code, filter.Query());
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
