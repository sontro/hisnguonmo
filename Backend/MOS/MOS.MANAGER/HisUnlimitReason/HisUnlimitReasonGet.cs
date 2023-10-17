using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitReason
{
    partial class HisUnlimitReasonGet : BusinessBase
    {
        internal HisUnlimitReasonGet()
            : base()
        {

        }

        internal HisUnlimitReasonGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_UNLIMIT_REASON> Get(HisUnlimitReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUnlimitReasonDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_UNLIMIT_REASON GetById(long id)
        {
            try
            {
                return GetById(id, new HisUnlimitReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_UNLIMIT_REASON GetById(long id, HisUnlimitReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUnlimitReasonDAO.GetById(id, filter.Query());
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
