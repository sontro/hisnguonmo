using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationResult
{
    partial class HisVaccinationResultGet : BusinessBase
    {
        internal HisVaccinationResultGet()
            : base()
        {

        }

        internal HisVaccinationResultGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_RESULT> Get(HisVaccinationResultFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationResultDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_RESULT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationResultFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_RESULT GetById(long id, HisVaccinationResultFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationResultDAO.GetById(id, filter.Query());
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
