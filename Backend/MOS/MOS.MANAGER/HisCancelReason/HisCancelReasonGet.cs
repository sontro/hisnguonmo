using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCancelReason
{
    partial class HisCancelReasonGet : BusinessBase
    {
        internal HisCancelReasonGet()
            : base()
        {

        }

        internal HisCancelReasonGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CANCEL_REASON> Get(HisCancelReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCancelReasonDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CANCEL_REASON GetById(long id)
        {
            try
            {
                return GetById(id, new HisCancelReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CANCEL_REASON GetById(long id, HisCancelReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCancelReasonDAO.GetById(id, filter.Query());
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
