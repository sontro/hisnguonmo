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
        internal HisMediContractMetyGet()
            : base()
        {

        }

        internal HisMediContractMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_CONTRACT_METY> Get(HisMediContractMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_CONTRACT_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediContractMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_CONTRACT_METY GetById(long id, HisMediContractMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMetyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_CONTRACT_METY> GetByMedicalContractId(long medicalContractId)
        {
            try
            {
                HisMediContractMetyFilterQuery filter = new HisMediContractMetyFilterQuery();
                filter.MEDICAL_CONTRACT_ID = medicalContractId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_CONTRACT_METY> GetByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMetyFilterQuery filter = new HisMediContractMetyFilterQuery();
                    filter.MEDICAL_CONTRACT_IDs = medicalContractIds;
                    return this.Get(filter);
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
