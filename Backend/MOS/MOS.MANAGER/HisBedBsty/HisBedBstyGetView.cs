using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyGet : BusinessBase
    {
        internal List<V_HIS_BED_BSTY> GetView(HisBedBstyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedBstyDAO.GetView(filter.Query(), param);
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
