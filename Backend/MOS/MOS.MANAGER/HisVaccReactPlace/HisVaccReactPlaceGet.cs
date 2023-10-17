using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    partial class HisVaccReactPlaceGet : BusinessBase
    {
        internal HisVaccReactPlaceGet()
            : base()
        {

        }

        internal HisVaccReactPlaceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACC_REACT_PLACE> Get(HisVaccReactPlaceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactPlaceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_REACT_PLACE GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccReactPlaceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_REACT_PLACE GetById(long id, HisVaccReactPlaceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactPlaceDAO.GetById(id, filter.Query());
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
