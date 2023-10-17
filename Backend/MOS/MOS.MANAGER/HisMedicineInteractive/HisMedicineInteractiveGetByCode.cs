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
        internal HIS_MEDICINE_INTERACTIVE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMedicineInteractiveFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_INTERACTIVE GetByCode(string code, HisMedicineInteractiveFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineInteractiveDAO.GetByCode(code, filter.Query());
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
