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
        internal V_HIS_VACC_REACT_PLACE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisVaccReactPlaceViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_VACC_REACT_PLACE GetViewByCode(string code, HisVaccReactPlaceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactPlaceDAO.GetViewByCode(code, filter.Query());
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
