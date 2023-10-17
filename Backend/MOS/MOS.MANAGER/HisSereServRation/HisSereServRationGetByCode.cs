using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    partial class HisSereServRationGet : BusinessBase
    {
        internal HIS_SERE_SERV_RATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSereServRationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_RATION GetByCode(string code, HisSereServRationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServRationDAO.GetByCode(code, filter.Query());
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
