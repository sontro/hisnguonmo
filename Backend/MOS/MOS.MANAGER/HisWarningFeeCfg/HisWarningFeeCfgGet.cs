using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgGet : BusinessBase
    {
        internal HisWarningFeeCfgGet()
            : base()
        {

        }

        internal HisWarningFeeCfgGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_WARNING_FEE_CFG> Get(HisWarningFeeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWarningFeeCfgDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WARNING_FEE_CFG GetById(long id)
        {
            try
            {
                return GetById(id, new HisWarningFeeCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WARNING_FEE_CFG GetById(long id, HisWarningFeeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWarningFeeCfgDAO.GetById(id, filter.Query());
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
