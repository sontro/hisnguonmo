using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionGet : BusinessBase
    {
        internal HIS_TRANSFUSION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTransfusionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION GetByCode(string code, HisTransfusionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionDAO.GetByCode(code, filter.Query());
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
