using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedType
{
    partial class HisBedTypeGet : BusinessBase
    {
        internal List<V_HIS_BED_TYPE> GetView(HisBedTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedTypeDAO.GetView(filter.Query(), param);
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
