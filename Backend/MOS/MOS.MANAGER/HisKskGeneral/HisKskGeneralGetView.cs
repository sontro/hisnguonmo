using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskGeneral
{
    partial class HisKskGeneralGet : BusinessBase
    {
        internal List<V_HIS_KSK_GENERAL> GetView(HisKskGeneralViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskGeneralDAO.GetView(filter.Query(), param);
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
