using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgGet : BusinessBase
    {
        internal HisExmeReasonCfgGet()
            : base()
        {

        }

        internal HisExmeReasonCfgGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXME_REASON_CFG> Get(HisExmeReasonCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExmeReasonCfgDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXME_REASON_CFG GetById(long id)
        {
            try
            {
                return GetById(id, new HisExmeReasonCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXME_REASON_CFG GetById(long id, HisExmeReasonCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExmeReasonCfgDAO.GetById(id, filter.Query());
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
