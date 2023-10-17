using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactType
{
    partial class HisVaccReactTypeGet : BusinessBase
    {
        internal HisVaccReactTypeGet()
            : base()
        {

        }

        internal HisVaccReactTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACC_REACT_TYPE> Get(HisVaccReactTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_REACT_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccReactTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_REACT_TYPE GetById(long id, HisVaccReactTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactTypeDAO.GetById(id, filter.Query());
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
