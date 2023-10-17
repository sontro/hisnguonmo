using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineInteractive
{
    partial class HisMedicineInteractiveGet : BusinessBase
    {
        internal V_HIS_MEDICINE_INTERACTIVE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMedicineInteractiveViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICINE_INTERACTIVE GetViewByCode(string code, HisMedicineInteractiveViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineInteractiveDAO.GetViewByCode(code, filter.Query());
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
