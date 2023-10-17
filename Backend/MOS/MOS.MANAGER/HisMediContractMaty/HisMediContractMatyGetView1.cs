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
        internal List<V_HIS_MEDI_CONTRACT_MATY_1> GetView1(HisMediContractMatyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMatyDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_CONTRACT_MATY_1> GetView1ByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMatyView1FilterQuery filter = new HisMediContractMatyView1FilterQuery();
                    filter.MEDICAL_CONTRACT_IDs = medicalContractIds;
                    return this.GetView1(filter);
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
