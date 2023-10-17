using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccineType
{
    partial class HisVaccineTypeGet : BusinessBase
    {
        internal HisVaccineTypeGet()
            : base()
        {

        }

        internal HisVaccineTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINE_TYPE> Get(HisVaccineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccineTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccineTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINE_TYPE GetById(long id, HisVaccineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccineTypeDAO.GetById(id, filter.Query());
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
