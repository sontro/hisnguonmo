using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceGet : BusinessBase
    {
        internal HIS_NONE_MEDI_SERVICE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisNoneMediServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NONE_MEDI_SERVICE GetByCode(string code, HisNoneMediServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNoneMediServiceDAO.GetByCode(code, filter.Query());
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
