using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContraindication
{
    partial class HisContraindicationGet : BusinessBase
    {
        internal HIS_CONTRAINDICATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisContraindicationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTRAINDICATION GetByCode(string code, HisContraindicationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContraindicationDAO.GetByCode(code, filter.Query());
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
