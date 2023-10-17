using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeGet : BusinessBase
    {
        internal HIS_STENT_CONCLUDE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisStentConcludeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STENT_CONCLUDE GetByCode(string code, HisStentConcludeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStentConcludeDAO.GetByCode(code, filter.Query());
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
