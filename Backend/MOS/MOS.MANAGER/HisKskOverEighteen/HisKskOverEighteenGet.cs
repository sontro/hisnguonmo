using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOverEighteen
{
    partial class HisKskOverEighteenGet : BusinessBase
    {
        internal HisKskOverEighteenGet()
            : base()
        {

        }

        internal HisKskOverEighteenGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_OVER_EIGHTEEN> Get(HisKskOverEighteenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOverEighteenDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OVER_EIGHTEEN GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskOverEighteenFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OVER_EIGHTEEN GetById(long id, HisKskOverEighteenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOverEighteenDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_OVER_EIGHTEEN> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskOverEighteenFilterQuery filter = new HisKskOverEighteenFilterQuery();
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
