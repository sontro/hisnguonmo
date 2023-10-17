using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataGet : BusinessBase
    {
        internal List<V_HIS_FORM_TYPE_CFG_DATA> GetView(HisFormTypeCfgDataViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDataDAO.GetView(filter.Query(), param);
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
