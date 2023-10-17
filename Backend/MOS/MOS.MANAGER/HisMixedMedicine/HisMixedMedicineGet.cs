using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineGet : BusinessBase
    {
        internal HisMixedMedicineGet()
            : base()
        {

        }

        internal HisMixedMedicineGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MIXED_MEDICINE> Get(HisMixedMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMixedMedicineDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MIXED_MEDICINE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMixedMedicineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MIXED_MEDICINE GetById(long id, HisMixedMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMixedMedicineDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MIXED_MEDICINE> GetByInfusionId(long id)
        {
            try
            {
                HisMixedMedicineFilterQuery filter = new HisMixedMedicineFilterQuery();
                filter.INFUSION_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MIXED_MEDICINE> GetByInfusionIds(List<long> ids)
        {
            try
            {
                HisMixedMedicineFilterQuery filter = new HisMixedMedicineFilterQuery();
                filter.INFUSION_IDs = ids;
                return this.Get(filter);
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
