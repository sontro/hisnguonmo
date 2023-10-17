using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceSame
{
    partial class HisServiceSameGet : BusinessBase
    {
        internal HisServiceSameGet()
            : base()
        {

        }

        internal HisServiceSameGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_SAME> Get(HisServiceSameFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceSameDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_SAME GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceSameFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_SAME GetById(long id, HisServiceSameFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceSameDAO.GetById(id, filter.Query());
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
