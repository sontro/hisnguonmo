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
        internal HIS_VACC_REACT_PLACE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccReactPlaceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_REACT_PLACE GetByCode(string code, HisVaccReactPlaceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactPlaceDAO.GetByCode(code, filter.Query());
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
