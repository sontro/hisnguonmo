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
        internal HIS_MEDI_REACT_SUM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMediReactSumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_REACT_SUM GetByCode(string code, HisMediReactSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediReactSumDAO.GetByCode(code, filter.Query());
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
