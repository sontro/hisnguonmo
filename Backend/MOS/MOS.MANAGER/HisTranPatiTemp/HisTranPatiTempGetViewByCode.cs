using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempGet : BusinessBase
    {
        internal V_HIS_TRAN_PATI_TEMP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTranPatiTempViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRAN_PATI_TEMP GetViewByCode(string code, HisTranPatiTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTempDAO.GetViewByCode(code, filter.Query());
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
