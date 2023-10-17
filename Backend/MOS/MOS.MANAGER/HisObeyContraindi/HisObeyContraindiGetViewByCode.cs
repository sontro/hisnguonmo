using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiGet : BusinessBase
    {
        internal V_HIS_OBEY_CONTRAINDI GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisObeyContraindiViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_OBEY_CONTRAINDI GetViewByCode(string code, HisObeyContraindiViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisObeyContraindiDAO.GetViewByCode(code, filter.Query());
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
