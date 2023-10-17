using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailGet : BusinessBase
    {
        internal V_HIS_SURG_REMU_DETAIL GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSurgRemuDetailViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SURG_REMU_DETAIL GetViewByCode(string code, HisSurgRemuDetailViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemuDetailDAO.GetViewByCode(code, filter.Query());
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
