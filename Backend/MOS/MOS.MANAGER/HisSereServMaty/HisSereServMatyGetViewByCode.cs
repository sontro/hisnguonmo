using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServMaty
{
    partial class HisSereServMatyGet : BusinessBase
    {
        internal V_HIS_SERE_SERV_MATY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSereServMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_MATY GetViewByCode(string code, HisSereServMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServMatyDAO.GetViewByCode(code, filter.Query());
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
