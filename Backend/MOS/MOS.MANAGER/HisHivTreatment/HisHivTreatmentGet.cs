using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentGet : BusinessBase
    {
        internal HisHivTreatmentGet()
            : base()
        {

        }

        internal HisHivTreatmentGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HIV_TREATMENT> Get(HisHivTreatmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHivTreatmentDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HIV_TREATMENT GetById(long id)
        {
            try
            {
                return GetById(id, new HisHivTreatmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HIV_TREATMENT GetById(long id, HisHivTreatmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHivTreatmentDAO.GetById(id, filter.Query());
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
