using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyGet : BusinessBase
    {
        internal HIS_ANTIGEN_METY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAntigenMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN_METY GetByCode(string code, HisAntigenMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenMetyDAO.GetByCode(code, filter.Query());
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
