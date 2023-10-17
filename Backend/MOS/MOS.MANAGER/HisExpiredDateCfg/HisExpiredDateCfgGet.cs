using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgGet : BusinessBase
    {
        internal HisExpiredDateCfgGet()
            : base()
        {

        }

        internal HisExpiredDateCfgGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXPIRED_DATE_CFG> Get(HisExpiredDateCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpiredDateCfgDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXPIRED_DATE_CFG GetById(long id)
        {
            try
            {
                return GetById(id, new HisExpiredDateCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXPIRED_DATE_CFG GetById(long id, HisExpiredDateCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpiredDateCfgDAO.GetById(id, filter.Query());
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
