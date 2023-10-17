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
        internal HIS_OBEY_CONTRAINDI GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisObeyContraindiFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_OBEY_CONTRAINDI GetByCode(string code, HisObeyContraindiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisObeyContraindiDAO.GetByCode(code, filter.Query());
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
