using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRereTime
{
    partial class HisServiceRereTimeGet : BusinessBase
    {
        internal HisServiceRereTimeGet()
            : base()
        {

        }

        internal HisServiceRereTimeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_RERE_TIME> Get(HisServiceRereTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceRereTimeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_RERE_TIME GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceRereTimeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_RERE_TIME GetById(long id, HisServiceRereTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceRereTimeDAO.GetById(id, filter.Query());
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
