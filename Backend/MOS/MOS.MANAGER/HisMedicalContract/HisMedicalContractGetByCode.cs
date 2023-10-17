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
        internal HIS_MEDICAL_CONTRACT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMedicalContractFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_CONTRACT GetByCode(string code, HisMedicalContractFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalContractDAO.GetByCode(code, filter.Query());
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
