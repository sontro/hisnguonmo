using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRemuneration
{
    partial class HisRemunerationGet : BusinessBase
    {
        internal HIS_REMUNERATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRemunerationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REMUNERATION GetByCode(string code, HisRemunerationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRemunerationDAO.GetByCode(code, filter.Query());
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
