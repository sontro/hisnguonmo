using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamGet : BusinessBase
    {
        internal List<V_HIS_BHYT_PARAM> GetView(HisBhytParamViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytParamDAO.GetView(filter.Query(), param);
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
