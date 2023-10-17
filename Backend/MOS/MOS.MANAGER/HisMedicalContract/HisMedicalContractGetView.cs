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
        internal List<V_HIS_MEDICAL_CONTRACT> GetView(HisMedicalContractViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalContractDAO.GetView(filter.Query(), param);
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
