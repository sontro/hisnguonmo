using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    partial class HisServiceNumOrderGet : BusinessBase
    {
        internal HisServiceNumOrderGet()
            : base()
        {

        }

        internal HisServiceNumOrderGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_NUM_ORDER> Get(HisServiceNumOrderFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceNumOrderDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_NUM_ORDER GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceNumOrderFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_NUM_ORDER GetById(long id, HisServiceNumOrderFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceNumOrderDAO.GetById(id, filter.Query());
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
