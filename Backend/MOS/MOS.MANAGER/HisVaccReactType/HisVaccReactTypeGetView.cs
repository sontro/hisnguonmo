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
        internal List<V_HIS_VACC_REACT_TYPE> GetView(HisVaccReactTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccReactTypeDAO.GetView(filter.Query(), param);
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
