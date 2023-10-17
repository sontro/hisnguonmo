using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    partial class HisDebateEkipUserGet : BusinessBase
    {
        internal HisDebateEkipUserGet()
            : base()
        {

        }

        internal HisDebateEkipUserGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBATE_EKIP_USER> Get(HisDebateEkipUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateEkipUserDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_EKIP_USER GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebateEkipUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_EKIP_USER GetById(long id, HisDebateEkipUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateEkipUserDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_DEBATE_EKIP_USER> GetByDebateId(long debateId)
        {
            try
            {
                HisDebateEkipUserFilterQuery filter = new HisDebateEkipUserFilterQuery();
                filter.DEBATE_ID = debateId;
                return this.Get(filter);
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
