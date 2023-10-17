using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminAGet : BusinessBase
    {
        internal List<V_HIS_VITAMIN_A> GetView(HisVitaminAViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVitaminADAO.GetView(filter.Query(), param);
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
