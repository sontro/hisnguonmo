using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    partial class HisVaccinationReactGet : BusinessBase
    {
        internal HisVaccinationReactGet()
            : base()
        {

        }

        internal HisVaccinationReactGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION_REACT> Get(HisVaccinationReactFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationReactDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_REACT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationReactFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_REACT GetById(long id, HisVaccinationReactFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationReactDAO.GetById(id, filter.Query());
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
