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
        internal HIS_CANCEL_REASON GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisCancelReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CANCEL_REASON GetByCode(string code, HisCancelReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCancelReasonDAO.GetByCode(code, filter.Query());
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
