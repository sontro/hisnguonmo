using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdService
{
    partial class HisIcdServiceGet : BusinessBase
    {
        internal HIS_ICD_SERVICE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisIcdServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ICD_SERVICE GetByCode(string code, HisIcdServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdServiceDAO.GetByCode(code, filter.Query());
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
