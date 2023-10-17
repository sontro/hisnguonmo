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
        internal V_HIS_SERE_SERV_RATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSereServRationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_RATION GetViewByCode(string code, HisSereServRationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServRationDAO.GetViewByCode(code, filter.Query());
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
