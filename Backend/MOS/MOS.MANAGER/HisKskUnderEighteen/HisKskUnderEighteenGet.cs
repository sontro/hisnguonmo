using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenGet : BusinessBase
    {
        internal HisKskUnderEighteenGet()
            : base()
        {

        }

        internal HisKskUnderEighteenGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_UNDER_EIGHTEEN> Get(HisKskUnderEighteenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskUnderEighteenDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_UNDER_EIGHTEEN GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskUnderEighteenFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_UNDER_EIGHTEEN GetById(long id, HisKskUnderEighteenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskUnderEighteenDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_UNDER_EIGHTEEN> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskUnderEighteenFilterQuery filter = new HisKskUnderEighteenFilterQuery();
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
