using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenGet : BusinessBase
    {
        internal HIS_ANTIGEN GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntigenFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN GetByCode(string code, HisAntigenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenDAO.GetByCode(code, filter.Query());
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
