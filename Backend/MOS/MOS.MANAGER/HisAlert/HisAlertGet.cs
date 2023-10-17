using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertGet : BusinessBase
    {
        internal HisAlertGet()
            : base()
        {

        }

        internal HisAlertGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ALERT> Get(HisAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAlertDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALERT GetById(long id)
        {
            try
            {
                return GetById(id, new HisAlertFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALERT GetById(long id, HisAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAlertDAO.GetById(id, filter.Query());
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
