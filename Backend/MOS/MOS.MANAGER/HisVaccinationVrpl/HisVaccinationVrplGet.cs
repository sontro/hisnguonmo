using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    partial class HisVaccinationVrplGet : BusinessBase
    {
        internal HisVaccinationVrplGet()
            : base()
        {

        }

        internal HisVaccinationVrplGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_VRPL> Get(HisVaccinationVrplFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrplDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_VRPL GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationVrplFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_VRPL GetById(long id, HisVaccinationVrplFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationVrplDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION_VRPL> GetByVaccinationId(long vaccinationId)
        {
            try
            {
                HisVaccinationVrplFilterQuery filter = new HisVaccinationVrplFilterQuery();
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

        internal List<HIS_VACCINATION_VRPL> GetByVaccReactPlaceId(long vaccReactPlaceId)
        {
            try
            {
                HisVaccinationVrplFilterQuery filter = new HisVaccinationVrplFilterQuery();
                filter.VACC_REACT_PLACE_ID = vaccReactPlaceId;
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
