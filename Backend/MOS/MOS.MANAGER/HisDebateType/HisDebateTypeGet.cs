using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateType
{
    partial class HisDebateTypeGet : BusinessBase
    {
        internal HisDebateTypeGet()
            : base()
        {

        }

        internal HisDebateTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBATE_TYPE> Get(HisDebateTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebateTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_TYPE GetById(long id, HisDebateTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateTypeDAO.GetById(id, filter.Query());
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
