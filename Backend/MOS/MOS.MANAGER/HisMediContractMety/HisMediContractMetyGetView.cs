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
        internal List<V_HIS_MEDI_CONTRACT_METY> GetView(HisMediContractMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMetyDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<V_HIS_MEDI_CONTRACT_METY> GetViewByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMetyViewFilterQuery filter = new HisMediContractMetyViewFilterQuery();
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
