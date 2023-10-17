using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBaby
{
    partial class HisBabyGet : BusinessBase
    {
        internal HisBabyGet()
            : base()
        {

        }

        internal HisBabyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BABY> Get(HisBabyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBabyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BABY GetById(long id)
        {
            try
            {
                return GetById(id, new HisBabyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BABY GetById(long id, HisBabyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBabyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BABY> GetByBornPositionId(long bornPositionId)
        {
            HisBabyFilterQuery filter = new HisBabyFilterQuery();
            filter.BORN_POSITION_ID = bornPositionId;
            return this.Get(filter);
        }

        internal List<HIS_BABY> GetByBornResultId(long bornResultId)
        {
            HisBabyFilterQuery filter = new HisBabyFilterQuery();
            filter.BORN_RESULT_ID = bornResultId;
            return this.Get(filter);
        }

        internal List<HIS_BABY> GetByBornTypeId(long bornTypeId)
        {
            HisBabyFilterQuery filter = new HisBabyFilterQuery();
            filter.BORN_TYPE_ID = bornTypeId;
            return this.Get(filter);
        }

        internal List<HIS_BABY> GetByTreatmentId(long treatmentId)
        {
            HisBabyFilterQuery filter = new HisBabyFilterQuery();
            filter.TREATMENT_ID = treatmentId;
            return this.Get(filter);
        }
    }
}
