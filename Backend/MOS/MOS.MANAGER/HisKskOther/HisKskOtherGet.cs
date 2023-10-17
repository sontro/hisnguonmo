using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherGet : BusinessBase
    {
        internal HisKskOtherGet()
            : base()
        {

        }

        internal HisKskOtherGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_OTHER> Get(HisKskOtherFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOtherDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OTHER GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskOtherFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OTHER GetById(long id, HisKskOtherFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOtherDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_OTHER> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskOtherFilterQuery filter = new HisKskOtherFilterQuery();
                filter.SERVICE_REQ_ID = serviceReqId;
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
