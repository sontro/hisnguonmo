using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateTemp
{
    partial class HisDebateTempGet : BusinessBase
    {
        internal HisDebateTempGet()
            : base()
        {

        }

        internal HisDebateTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBATE_TEMP> Get(HisDebateTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebateTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_TEMP GetById(long id, HisDebateTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateTempDAO.GetById(id, filter.Query());
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
