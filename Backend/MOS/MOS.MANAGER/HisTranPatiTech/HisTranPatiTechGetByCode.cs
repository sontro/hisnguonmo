using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTech
{
    partial class HisTranPatiTechGet : BusinessBase
    {
        internal HIS_TRAN_PATI_TECH GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTranPatiTechFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TECH GetByCode(string code, HisTranPatiTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTechDAO.GetByCode(code, filter.Query());
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
