using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRejectAlert
{
    partial class HisRejectAlertGet : BusinessBase
    {
        internal HisRejectAlertGet()
            : base()
        {

        }

        internal HisRejectAlertGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_REJECT_ALERT> Get(HisRejectAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRejectAlertDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REJECT_ALERT GetById(long id)
        {
            try
            {
                return GetById(id, new HisRejectAlertFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REJECT_ALERT GetById(long id, HisRejectAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRejectAlertDAO.GetById(id, filter.Query());
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
