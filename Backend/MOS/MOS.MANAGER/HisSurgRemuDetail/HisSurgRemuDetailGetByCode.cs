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
        internal HIS_SURG_REMU_DETAIL GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSurgRemuDetailFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMU_DETAIL GetByCode(string code, HisSurgRemuDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemuDetailDAO.GetByCode(code, filter.Query());
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
