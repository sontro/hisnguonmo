using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationGet : BusinessBase
    {
        internal HIS_SURG_REMUNERATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSurgRemunerationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMUNERATION GetByCode(string code, HisSurgRemunerationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemunerationDAO.GetByCode(code, filter.Query());
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
