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
        internal V_HIS_TRANSFUSION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTransfusionViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANSFUSION GetViewByCode(string code, HisTransfusionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionDAO.GetViewByCode(code, filter.Query());
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
