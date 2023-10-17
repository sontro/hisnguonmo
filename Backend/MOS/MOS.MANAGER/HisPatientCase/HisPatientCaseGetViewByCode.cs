using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientCase
{
    partial class HisPatientCaseGet : BusinessBase
    {
        internal V_HIS_PATIENT_CASE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPatientCaseViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PATIENT_CASE GetViewByCode(string code, HisPatientCaseViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientCaseDAO.GetViewByCode(code, filter.Query());
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
