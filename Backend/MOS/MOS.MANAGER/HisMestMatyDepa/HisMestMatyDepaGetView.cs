using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMatyDepa
{
    partial class HisMestMatyDepaGet : BusinessBase
    {
        internal List<V_HIS_MEST_MATY_DEPA> GetView(HisMestMatyDepaViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMatyDepaDAO.GetView(filter.Query(), param);
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
