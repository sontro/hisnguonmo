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
        internal HIS_TRAN_PATI_TEMP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTranPatiTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TEMP GetByCode(string code, HisTranPatiTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTempDAO.GetByCode(code, filter.Query());
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
