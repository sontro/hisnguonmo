using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    partial class HisTransfusionSumGet : BusinessBase
    {
        internal HisTransfusionSumGet()
            : base()
        {

        }

        internal HisTransfusionSumGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRANSFUSION_SUM> Get(HisTransfusionSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionSumDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION_SUM GetById(long id)
        {
            try
            {
                return GetById(id, new HisTransfusionSumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION_SUM GetById(long id, HisTransfusionSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionSumDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANSFUSION_SUM> GetByTreatmentId(long treatmentId)
        {
            try
            {
                HisTransfusionSumFilterQuery filter = new HisTransfusionSumFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANSFUSION_SUM> GetByExpMestBloodId(long expMestBloodId)
        {
            try
            {
                HisTransfusionSumFilterQuery filter = new HisTransfusionSumFilterQuery();
                filter.EXP_MEST_BLOOD_ID = expMestBloodId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANSFUSION_SUM> GetByExpMestBloodIds(List<long> expMestBloodIds)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestBloodIds))
                {
                    HisTransfusionSumFilterQuery filter = new HisTransfusionSumFilterQuery();
                    filter.EXP_MEST_BLOOD_IDs = expMestBloodIds;
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
