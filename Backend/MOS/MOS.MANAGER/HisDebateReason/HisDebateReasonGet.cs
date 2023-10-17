using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    partial class HisDebateReasonGet : BusinessBase
    {
        internal HisDebateReasonGet()
            : base()
        {

        }

        internal HisDebateReasonGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBATE_REASON> Get(HisDebateReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateReasonDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_REASON GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebateReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_REASON GetById(long id, HisDebateReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateReasonDAO.GetById(id, filter.Query());
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
