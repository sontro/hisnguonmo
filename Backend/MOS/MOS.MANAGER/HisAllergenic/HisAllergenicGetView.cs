using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    partial class HisAllergenicGet : BusinessBase
    {
        internal List<V_HIS_ALLERGENIC> GetView(HisAllergenicViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAllergenicDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ALLERGENIC GetViewById(long id)
        {
            try
            {
                return DAOWorker.HisAllergenicDAO.GetViewById(id, new HisAllergenicViewFilterQuery().Query());
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
