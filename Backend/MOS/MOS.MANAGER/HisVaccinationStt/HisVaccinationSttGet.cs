using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttGet : BusinessBase
    {
        internal HisVaccinationSttGet()
            : base()
        {

        }

        internal HisVaccinationSttGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_STT> Get(HisVaccinationSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationSttDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_STT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_STT GetById(long id, HisVaccinationSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationSttDAO.GetById(id, filter.Query());
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
