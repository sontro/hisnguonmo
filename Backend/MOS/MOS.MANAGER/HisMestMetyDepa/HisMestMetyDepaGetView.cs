using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyDepa
{
    partial class HisMestMetyDepaGet : BusinessBase
    {
        internal List<V_HIS_MEST_METY_DEPA> GetView(HisMestMetyDepaViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyDepaDAO.GetView(filter.Query(), param);
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
