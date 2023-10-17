using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnGet : BusinessBase
    {
        internal HisHoldReturnGet()
            : base()
        {

        }

        internal HisHoldReturnGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HOLD_RETURN> Get(HisHoldReturnFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoldReturnDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HOLD_RETURN GetById(long id)
        {
            try
            {
                return GetById(id, new HisHoldReturnFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HOLD_RETURN GetById(long id, HisHoldReturnFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoldReturnDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_HOLD_RETURN> GetByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisHoldReturnFilterQuery filter = new HisHoldReturnFilterQuery();
                    filter.IDs = ids;
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

        internal List<HIS_HOLD_RETURN> GetByTreatmentId(long id)
        {
            try
            {
                HisHoldReturnFilterQuery filter = new HisHoldReturnFilterQuery();
                filter.TREATMENT_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal object GetByPatientId(long treatmentId)
        {
            throw new NotImplementedException();
        }
    }
}
