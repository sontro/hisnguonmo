using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMaty
{
    partial class HisMediContractMatyGet : BusinessBase
    {
        internal List<V_HIS_MEDI_CONTRACT_MATY> GetView(HisMediContractMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMatyDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_CONTRACT_MATY> GetViewByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMatyViewFilterQuery filter = new HisMediContractMatyViewFilterQuery();
                    filter.MEDICAL_CONTRACT_IDs = medicalContractIds;
                    return this.GetView(filter);
                }
                return null;
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
