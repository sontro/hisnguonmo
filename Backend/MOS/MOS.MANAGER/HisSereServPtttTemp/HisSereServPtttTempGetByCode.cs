using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    partial class HisSereServPtttTempGet : BusinessBase
    {
        internal HIS_SERE_SERV_PTTT_TEMP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSereServPtttTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_PTTT_TEMP GetByCode(string code, HisSereServPtttTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServPtttTempDAO.GetByCode(code, filter.Query());
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
