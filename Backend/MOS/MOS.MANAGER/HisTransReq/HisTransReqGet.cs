using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    partial class HisTransReqGet : BusinessBase
    {
        internal HisTransReqGet()
            : base()
        {

        }

        internal HisTransReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRANS_REQ> Get(HisTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransReqDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANS_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisTransReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANS_REQ GetById(long id, HisTransReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransReqDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANS_REQ> GetByTreatmentId(long id)
        {
            HisTransReqFilterQuery filter = new HisTransReqFilterQuery();
            filter.TREATMENT_ID = id;
            return this.Get(filter);
        }
    }
}
