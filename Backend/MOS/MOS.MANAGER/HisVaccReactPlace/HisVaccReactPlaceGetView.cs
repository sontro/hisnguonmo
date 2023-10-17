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
        internal List<V_HIS_VACC_REACT_PLACE> GetView(HisVaccReactPlaceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactPlaceDAO.GetView(filter.Query(), param);
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
