using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamServiceTemp
{
    partial class HisExamServiceTempGet : BusinessBase
    {
        internal V_HIS_EXAM_SERVICE_TEMP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExamServiceTempViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXAM_SERVICE_TEMP GetViewByCode(string code, HisExamServiceTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExamServiceTempDAO.GetViewByCode(code, filter.Query());
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
