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
        internal HIS_SERE_SERV_MATY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSereServMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_MATY GetByCode(string code, HisSereServMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServMatyDAO.GetByCode(code, filter.Query());
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
