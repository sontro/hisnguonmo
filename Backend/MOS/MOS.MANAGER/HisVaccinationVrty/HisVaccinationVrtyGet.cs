using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    partial class HisVaccinationVrtyGet : BusinessBase
    {
        internal HisVaccinationVrtyGet()
            : base()
        {

        }

        internal HisVaccinationVrtyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_VRTY> Get(HisVaccinationVrtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrtyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_VRTY GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationVrtyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_VRTY GetById(long id, HisVaccinationVrtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrtyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION_VRTY> GetByVaccinationId(long vaccinationId)
        {
            try
            {
                HisVaccinationVrtyFilterQuery filter = new HisVaccinationVrtyFilterQuery();
                filter.VACCINATION_ID = vaccinationId;
                return Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION_VRTY> GetByVaccReactTypeId(long vaccReactTypeId)
        {
            try
            {
                HisVaccinationVrtyFilterQuery filter = new HisVaccinationVrtyFilterQuery();
                filter.VACC_REACT_TYPE_ID = vaccReactTypeId;
                return Get(filter);
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
