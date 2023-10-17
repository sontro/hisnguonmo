using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactSum
{
    partial class HisMediReactSumGet : BusinessBase
    {
        internal V_HIS_MEDI_REACT_SUM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMediReactSumViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_REACT_SUM GetViewByCode(string code, HisMediReactSumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediReactSumDAO.GetViewByCode(code, filter.Query());
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
