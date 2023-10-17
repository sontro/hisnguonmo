using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    partial class HisPtttTableGet : BusinessBase
    {
        internal HIS_PTTT_TABLE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPtttTableFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_TABLE GetByCode(string code, HisPtttTableFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttTableDAO.GetByCode(code, filter.Query());
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
