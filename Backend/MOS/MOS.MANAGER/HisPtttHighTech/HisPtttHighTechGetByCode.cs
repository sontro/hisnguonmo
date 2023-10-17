using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttHighTech
{
    partial class HisPtttHighTechGet : BusinessBase
    {
        internal HIS_PTTT_HIGH_TECH GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPtttHighTechFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_HIGH_TECH GetByCode(string code, HisPtttHighTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttHighTechDAO.GetByCode(code, filter.Query());
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
