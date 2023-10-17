using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeGet : BusinessBase
    {
        internal HisStentConcludeGet()
            : base()
        {

        }

        internal HisStentConcludeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_STENT_CONCLUDE> Get(HisStentConcludeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStentConcludeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STENT_CONCLUDE GetById(long id)
        {
            try
            {
                return GetById(id, new HisStentConcludeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STENT_CONCLUDE GetById(long id, HisStentConcludeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStentConcludeDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STENT_CONCLUDE GetBySereServId(long sereServId)
        {
            try
            {
                HisStentConcludeFilterQuery filter = new HisStentConcludeFilterQuery();
                filter.SERE_SERV_ID = sereServId;
                List<HIS_STENT_CONCLUDE> lst = this.Get(filter);
                return IsNotNullOrEmpty(lst) ? lst[0] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_STENT_CONCLUDE> GetBySereServIds(List<long> sereServIds)
        {
            try
            {
                HisStentConcludeFilterQuery filter = new HisStentConcludeFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
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
