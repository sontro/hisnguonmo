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
        internal HisMediContractMatyGet()
            : base()
        {

        }

        internal HisMediContractMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_CONTRACT_MATY> Get(HisMediContractMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_CONTRACT_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediContractMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_CONTRACT_MATY GetById(long id, HisMediContractMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediContractMatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_MEDI_CONTRACT_MATY> GetByMedicalContractId(long medicalContractId)
        {
            try
            {
                HisMediContractMatyFilterQuery filter = new HisMediContractMatyFilterQuery();
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

        internal List<HIS_MEDI_CONTRACT_MATY> GetByMedicalContractIds(List<long> medicalContractIds)
        {
            try
            {
                if (IsNotNullOrEmpty(medicalContractIds))
                {
                    HisMediContractMatyFilterQuery filter = new HisMediContractMatyFilterQuery();
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
