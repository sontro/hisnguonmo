using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfg
{
    partial class HisFormTypeCfgGet : BusinessBase
    {
        internal HIS_FORM_TYPE_CFG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisFormTypeCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FORM_TYPE_CFG GetByCode(string code, HisFormTypeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDAO.GetByCode(code, filter.Query());
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
