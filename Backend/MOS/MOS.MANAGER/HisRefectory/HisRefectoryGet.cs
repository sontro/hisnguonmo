using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryGet : BusinessBase
    {
        internal HisRefectoryGet()
            : base()
        {

        }

        internal HisRefectoryGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_REFECTORY> Get(HisRefectoryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRefectoryDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REFECTORY GetById(long id)
        {
            try
            {
                return GetById(id, new HisRefectoryFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REFECTORY GetById(long id, HisRefectoryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRefectoryDAO.GetById(id, filter.Query());
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
