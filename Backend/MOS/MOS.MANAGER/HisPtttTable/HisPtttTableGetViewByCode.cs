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
        internal V_HIS_PTTT_TABLE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPtttTableViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PTTT_TABLE GetViewByCode(string code, HisPtttTableViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttTableDAO.GetViewByCode(code, filter.Query());
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
