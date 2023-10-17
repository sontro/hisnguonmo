using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractGet : BusinessBase
    {
        internal V_HIS_MEDICAL_CONTRACT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMedicalContractViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICAL_CONTRACT GetViewByCode(string code, HisMedicalContractViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalContractDAO.GetViewByCode(code, filter.Query());
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
