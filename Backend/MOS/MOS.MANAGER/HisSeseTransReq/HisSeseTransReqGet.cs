using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqGet : BusinessBase
    {
        internal HisSeseTransReqGet()
            : base()
        {

        }

        internal HisSeseTransReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SESE_TRANS_REQ> Get(HisSeseTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseTransReqDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_TRANS_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisSeseTransReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_TRANS_REQ GetById(long id, HisSeseTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseTransReqDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SESE_TRANS_REQ> GetByTransReqId(long transReqId)
        {
            try
            {
                HisSeseTransReqFilterQuery filter = new HisSeseTransReqFilterQuery();
                filter.TRANS_REQ_ID = transReqId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        internal List<HIS_SESE_TRANS_REQ> GetByTransReqIds(List<long> transReqIds)
        {
            try
            {
                if (transReqIds != null)
                {
                    HisSeseTransReqFilterQuery filter = new HisSeseTransReqFilterQuery();
                    filter.TRANS_REQ_IDs = transReqIds;
                    return this.Get(filter);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        internal List<HIS_SESE_TRANS_REQ> GetBySereServIds(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSeseTransReqFilterQuery filter = new HisSeseTransReqFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                return this.Get(filter);
            }
            return null;
        }
    }
}
