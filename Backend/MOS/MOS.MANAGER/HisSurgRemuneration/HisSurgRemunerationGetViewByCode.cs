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
        internal V_HIS_SURG_REMUNERATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSurgRemunerationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SURG_REMUNERATION GetViewByCode(string code, HisSurgRemunerationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemunerationDAO.GetViewByCode(code, filter.Query());
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
