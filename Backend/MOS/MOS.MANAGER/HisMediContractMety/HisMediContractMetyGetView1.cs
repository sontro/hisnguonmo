using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    partial class HisMediContractMetyGet : BusinessBase
    {
        internal List<V_HIS_MEDI_CONTRACT_METY_1> GetView1(HisMediContractMetyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMetyDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<V_HIS_MEDI_CONTRACT_METY_1> GetView1ByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMetyView1FilterQuery filter = new HisMediContractMetyView1FilterQuery();
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
